using System;
using System.Threading;
using System.Threading.Tasks;
using AtmService.Models;
using AtmService.Repositories;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AtmService.Services;

public class TimedHostedService : IHostedService, IDisposable
{
    private readonly ILogger<TimedHostedService> _logger;
    private readonly IBus _bus;
    private Timer _timer;
    private readonly ICrudRepository<Atm> _atmRepository;
    private readonly ICrudRepository<AtmStatus> _atmStatusRepository;

    public TimedHostedService(ILogger<TimedHostedService> logger, IBus bus, ICrudRepository<Atm> atmRepository, ICrudRepository<AtmStatus> atmStatusRepository)
    {
        _logger = logger;
        _bus = bus;
        _atmRepository = atmRepository;
        _atmStatusRepository = atmStatusRepository;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Broker :: Timed Hosted Service running.");
        _timer = new Timer(VerifyAtmsStatus, null, TimeSpan.Zero, TimeSpan.FromMinutes(2.2));
        return Task.CompletedTask;
    }

    private void VerifyAtmsStatus(object state)
    {
        var atms = _atmRepository.GetAll().Result;
        foreach (var atm in atms)
        {
            _atmStatusRepository.Get(atm.Id).ContinueWith(task =>
            {
                if (task.Result == null)
                {
                    _logger.LogInformation($"Atm {atm.Id} is down");
                    _atmStatusRepository.Add(new AtmStatus
                    {
                        Id = atm.Id,
                        ErrorMessage = "Atm is down",
                        Date = DateTime.Now
                    });

                }
                else if (task.Result.Date.AddMinutes(2) < DateTime.Now)
                {
                    _logger.LogInformation($"Atm {atm.Id} is down by more than 2 minutes");
                    _atmStatusRepository.Add(new AtmStatus
                    {
                        Id = atm.Id,
                        ErrorMessage = "Atm is down by more than 2 minutes",
                        Date = DateTime.Now
                    });
                }
                else
                {
                    _logger.LogInformation($"Atm {atm.Id} is up");
                }
            });
        }

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
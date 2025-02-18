using AtmService.Models;
using AtmService.Repositories;
using MassTransit;
namespace AtmService.Services;

public class TimedHostedService : IHostedService, IDisposable
{
    private readonly ILogger<TimedHostedService> _logger;
    private readonly ICredentialRepository _credentialRepository;
    private readonly IBus _bus;
    private Timer _timer;
    private Guid _guidAtm;

    public TimedHostedService(ILogger<TimedHostedService> logger, IBus bus, ICredentialRepository credentialRepository)
    {
        _logger = logger;
        _bus = bus;
        _credentialRepository = credentialRepository;
        _guidAtm = _credentialRepository.ReadCredentials().Result;
        if(_guidAtm == Guid.Empty)
        {
            _guidAtm = Guid.NewGuid();
            _credentialRepository.WriteCredentials(_guidAtm);
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");
        _timer = new Timer(PublishEvent, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
        return Task.CompletedTask;
    }

    private void PublishEvent(object state)
    {
        _logger.LogInformation("Publishing event to RabbitMQ.");
        _bus.Publish(new AtmStatus { Id = _guidAtm, ErrorMessage = "No error", Date = DateTime.Now });
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
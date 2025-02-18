using AtmService.Models;
using AtmService.Repositories;
using MassTransit;

namespace AtmService.Services;

public class AtmEventConsumer : IConsumer<AtmStatus>
{
    private readonly ILogger<AtmEventConsumer> _logger;
    private readonly ICrudRepository<AtmStatus> _atmStatusRepository;
    private readonly ICrudRepository<Atm> _atmRepository;

    public AtmEventConsumer(ILogger<AtmEventConsumer> logger, ICrudRepository<AtmStatus> atmStatusRepository, ICrudRepository<Atm> atmRepository)
    {
        _logger = logger;
        _atmStatusRepository = atmStatusRepository;
        _atmRepository = atmRepository;
    }
   
    public Task Consume(ConsumeContext<AtmStatus> context)
    {
        _logger.LogInformation($"Received AtmStatus event with Id: {context.Message.Id}");
        _atmRepository.Get(context.Message.Id).ContinueWith(task =>
        {
            if (task.Result == null)
            {
                _logger.LogInformation($"Atm {context.Message.Id} is not found in the database, adding it");
                _atmRepository.Add(new Atm
                {
                    Id = context.Message.Id,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    Location = "Unknown",
                    AccountId = 0
                });
            }
        });
        _atmStatusRepository.Add(context.Message);
        return Task.CompletedTask;
    }
}
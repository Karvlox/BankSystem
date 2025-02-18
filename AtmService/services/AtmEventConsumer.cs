using AtmService.Models;
using MassTransit;

namespace AtmService.Services;

public class AtmEventConsumer : IConsumer<AtmStatus>
{
    private readonly ILogger<AtmEventConsumer> _logger;

    public AtmEventConsumer(ILogger<AtmEventConsumer> logger)
    {
        _logger = logger;
    }
    public Task Consume(ConsumeContext<AtmStatus> context)
    {
        _logger.LogInformation($"Received AtmStatus event with Id: {context.Message.Id}");
        return Task.CompletedTask;
    }
}
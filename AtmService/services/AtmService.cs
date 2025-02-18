using AtmService.Models;

namespace AtmService.Services;

public class AtmService : IAtmService
{
    public Task<AtmStatus> GetAtmStatus()
    {
        return Task.FromResult(new AtmStatus
        {
            Id = Guid.NewGuid(),
            ErrorMessage = "No error",
            Date = DateTime.Now
        });
    }
}
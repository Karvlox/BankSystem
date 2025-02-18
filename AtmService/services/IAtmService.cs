using AtmService.Models;

namespace AtmService.Services;

public interface IAtmService
{
    Task<AtmStatus> GetAtmStatus();
   
}
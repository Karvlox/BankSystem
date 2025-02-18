using System.Text;
using System.Text.Json;
using AtmService.Models;
using EmailService.Dto;

namespace AtmService.Repositories;

public class LogGmailRepository : ICrudRepository<AtmStatus>
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<LogGmailRepository> _logger;

     public LogGmailRepository(HttpClient httpClient, ILogger<LogGmailRepository> logger)
    {
        _httpClient = httpClient;
    }
    public async  Task Add(AtmStatus entity)
    {
        var emailDto = new EmailInputDto
        {
            email = Environment.GetEnvironmentVariable("GmailToSaveLogs"),
            subject = entity.Id.ToString(),
            message = JsonSerializer.Serialize(entity)
        };

        var json = JsonSerializer.Serialize(emailDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://messageservice:80/api/Email", content);
        _logger.LogInformation($"Email sent with status code: {response.StatusCode}");
        response.EnsureSuccessStatusCode();
    }

    public Task Delete(Guid id)
    {
        return Task.CompletedTask;
    }

    public async Task<AtmStatus> Get(Guid id)
    {
        var response = _httpClient.GetAsync($"http://messageservice:80/api/Email?subjectFilter={id}").Result;

        var json = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions 
        { 
            PropertyNameCaseInsensitive = true 
        };

        var emails = JsonSerializer.Deserialize<List<EmailOutputDto>>(json, options);
        // Get the email with the latest date
        var email = emails.OrderByDescending(e => e.Date).FirstOrDefault();
        return JsonSerializer.Deserialize<AtmStatus>(email.Body);
        
    }

    public Task<IEnumerable<AtmStatus>> GetAll()
    {
        return Task.FromResult(Enumerable.Empty<AtmStatus>());
    }

    public Task Update(AtmStatus entity)
    {
        return Task.CompletedTask;
    }
}
namespace MessageService.Services;

public interface IEmailService<TMessage>
{
    Task SendEmailAsync(string email, string subject, string message);
    Task<IEnumerable<TMessage>> GetEmailsAsync(string subjectFilter = null, string toFilter = null);
}
using System.Net;
using System.Net.Mail;
using MessageService.Dto;
using MessageService.Repositories;

namespace MessageService.Services;

public class GmailMessageService : IEmailService<EmailDto>
{
    private readonly IConfiguration _configuration;
    private readonly IEmailRepository<EmailDto> _emailRepository;
    public GmailMessageService(IConfiguration configuration, IEmailRepository<EmailDto> emailRepository)
    {
        _configuration = configuration;
        _emailRepository = emailRepository;
    }

    public Task<IEnumerable<EmailDto>> GetEmailsAsync( string subjectFilter = null, string toFilter = null)
    {
        return _emailRepository.GetEmailsAsync(subjectFilter, toFilter);
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var smtpSettings = _configuration.GetSection("SmtpSettings");
        var host = smtpSettings["Host"];
        var port = int.Parse(smtpSettings["Port"]);
        var enableSsl = bool.Parse(smtpSettings["EnableSsl"]);
        var fromAddress = Environment.GetEnvironmentVariable("EmailProvider") ?? smtpSettings["From"];
        var password = Environment.GetEnvironmentVariable("EmailProviderPassword") ?? smtpSettings["Password"];

        using var smtpClient = new SmtpClient(host)
        {
            Port = port,
            Credentials = new NetworkCredential(fromAddress, password),
            EnableSsl = enableSsl
        };

        using var mailMessage = new MailMessage(fromAddress, email)
        {
            Subject = subject,
            Body = message
        };

        await smtpClient.SendMailAsync(mailMessage);
    }
}
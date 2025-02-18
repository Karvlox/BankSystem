using MailKit.Net.Imap;
using MailKit;
using MimeKit;
using System.Threading.Tasks;
using MessageService.Dto;
using MailKit.Search;
using MessageService.Repositories;

namespace MessageService.Repositories;

public class ImapEmailRepository : IEmailRepository<EmailDto>
{

    private readonly string _imapServer;
    private readonly int _imapPort;
    private readonly string _username;
    private readonly string _password;

    public ImapEmailRepository()
    {
        _imapServer = Environment.GetEnvironmentVariable("ImapServer");
        _imapPort = int.Parse(Environment.GetEnvironmentVariable("ImapPort"));
        _username = Environment.GetEnvironmentVariable("EmailProvider");
        _password = Environment.GetEnvironmentVariable("EmailProviderPassword");
    }

    public async Task<IEnumerable<EmailDto>> GetEmailsAsync(string subjectFilter = null, string toFilter = null)
    {
        using var client = new ImapClient();
        await client.ConnectAsync(_imapServer, _imapPort, true);
        await client.AuthenticateAsync(_username, _password);

        var inbox = client.Inbox;
        await inbox.OpenAsync(FolderAccess.ReadOnly);

        var query = SearchQuery.All;
        if (!string.IsNullOrEmpty(subjectFilter))
        {
            query = query.And(SearchQuery.SubjectContains(subjectFilter));
        }
        if (!string.IsNullOrEmpty(toFilter))
        {
            query = query.And(SearchQuery.ToContains(toFilter));
        }

        var uids = await inbox.SearchAsync(query);
        var emails = new List<EmailDto>();
        foreach (var uid in uids)
        {
            var message = await inbox.GetMessageAsync(uid);
            var emailDto = new EmailDto
            {
                From = message.From.ToString(),
                To = message.To.ToString(),
                Subject = message.Subject,
                Body = message.TextBody,
                Date = message.Date.DateTime
            };
            emails.Add(emailDto);
        }

        await client.DisconnectAsync(true);
        return emails;
    }
}
using MessageService.Dto;

namespace MessageService.Repositories;

public interface IEmailRepository<TMessage>
{
    Task<IEnumerable<TMessage>> GetEmailsAsync(string subjectFilter, string toFilter    );
}
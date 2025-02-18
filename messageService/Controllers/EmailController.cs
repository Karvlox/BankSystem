using MessageService.Dto;
using MessageService.Services;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace MessageService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService<EmailDto> _emailService;
    public EmailController(IEmailService<EmailDto> emailService)
    {
        _emailService = emailService;
    }
    [HttpPost]
    public async Task<IActionResult> SendEmailAsync([FromBody] EmailRequest request)
    {
        await _emailService.SendEmailAsync(request.Email, request.Subject, request.Message);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetEmailsAsync(string subjectFilter = null, string toFilter = null)
    {
        var emails = await _emailService.GetEmailsAsync(subjectFilter, toFilter);
        return Ok(emails);
    }
}
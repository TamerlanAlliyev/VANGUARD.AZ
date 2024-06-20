using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Vanguard.Areas.Admin.Services.Interfaces;

public class EmailService:IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Send(string userEmail, string subject, string body, bool isBodyHtml = true, string? attachmentPath = null)
    {
        SmtpClient smtpClient = new SmtpClient
        {
            Port = Convert.ToInt32(_configuration["Email:Port"]),
            Host = _configuration["Email:Host"],
            EnableSsl = true,
            Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"])
        };

        MailMessage message = new MailMessage
        {
            From = new MailAddress(_configuration["Email:Username"], "Vanguard support"),
            Subject = subject,
            Body = body,
            IsBodyHtml = isBodyHtml
        };

        message.To.Add(new MailAddress(userEmail));

        if (!string.IsNullOrEmpty(attachmentPath))
        {
            Attachment attachment = new Attachment(attachmentPath);
            message.Attachments.Add(attachment);
        }

        smtpClient.Send(message);
    }
}

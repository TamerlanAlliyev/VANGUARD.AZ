namespace Vanguard.Areas.Admin.Services.Interfaces
{
    public interface IEmailService
    {
        public void Send(string to, string subject, string body, bool isHtml, string? attachmentPath = null);

    }
}

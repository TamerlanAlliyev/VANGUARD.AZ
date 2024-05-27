namespace Vanguard.Services.Interfaces;

public interface IEmailService
{
    public void Send(string userEmail, string Subject, string Body, bool IsBody = true);

}

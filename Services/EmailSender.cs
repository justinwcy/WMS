using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace WMS.Services;
public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;

    public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
        ILogger<EmailSender> logger)
    {
        Options = optionsAccessor.Value;
        _logger = logger;
    }

    public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        await Execute(subject, message, toEmail);
    }

    public async Task Execute(string subject, string message, string toEmail)
    {
        var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(Options.AdminEmail);
        mailMessage.To.Add(toEmail);
        mailMessage.Subject = subject;
        mailMessage.IsBodyHtml = true;
        mailMessage.Body = message;

        try
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = Options.SmtpPort;
            smtpClient.Host = Options.SmtpHost;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(Options.AdminEmail, Options.AdminPassword);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(mailMessage);

            _logger.LogInformation($"Email to {toEmail} sent!");
        }
        catch (Exception exception)
        {
            _logger.LogInformation($"Could not email to {toEmail}. Exception = {exception.Message}");
        }
    }
}
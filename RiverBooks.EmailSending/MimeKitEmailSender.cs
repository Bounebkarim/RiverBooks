using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using ILogger = Serilog.ILogger;

namespace RiverBooks.EmailSending;

public class MimeKitEmailSender(ILogger logger) : ISendEmail
{
  private readonly ILogger _logger = logger;

  public async Task SendEmailAsync(string to, string from, string subject, string body)
  {
    _logger.Information("Sending email to {to} from {from} with subject {subject}", to, from, subject);
    using var client = new SmtpClient();
    await client.ConnectAsync(Constants.EmailServer,25,false);
    var message = new MimeMessage();
    message.From.Add(MailboxAddress.Parse(from));
    message.To.Add(MailboxAddress.Parse(to));
    message.Subject = subject;
    message.Body = new TextPart("plain") { Text = body };
    await client.SendAsync(message);
    _logger.Information("Email sent !");
    client?.DisconnectAsync(true);
    _logger.Information("client disconnected.");
  }
}

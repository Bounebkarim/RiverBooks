
using Serilog;

namespace RiverBooks.EmailSending;

internal interface ISendEmailsFromOutboxService
{
  Task CheckForAndSendEmailsAsync();
}

internal class DefaultSendEmailsFromOutboxService(ILogger logger, IOutboxService outboxService, ISendEmail sendEmail)
  : ISendEmailsFromOutboxService
{
  private readonly ILogger _logger = logger;

  public async Task CheckForAndSendEmailsAsync()
  {
    var emails = await outboxService.GetEmailsToBeSentAsync();
    try
    {
      foreach (var email in emails.Value)
      {
        await sendEmail.SendEmailAsync(email.To, email.From, email.Subject, email.Body);
        await outboxService.MarkEmailAsSentAsync(email);
      }
      _logger.Information("Processed {count} email records.", emails.Value.Count);
    }
    finally
    {
      _logger.Information("Sleeping ...");
    }

  }
}

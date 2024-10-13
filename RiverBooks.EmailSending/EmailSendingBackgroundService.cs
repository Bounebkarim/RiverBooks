using Microsoft.Extensions.Hosting;
using Serilog;

namespace RiverBooks.EmailSending;

internal class EmailSendingBackgroundService(ILogger logger,ISendEmailsFromOutboxService sendEmailsFromOutboxService) : BackgroundService
{
  private readonly ILogger _logger = logger;
  private readonly ISendEmailsFromOutboxService _sendEmailsFromOutboxService = sendEmailsFromOutboxService;

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    int delayMilliseconds = 10_000;
    _logger.Information("{serviceName} starting",nameof(EmailSendingBackgroundService));

    while (!stoppingToken.IsCancellationRequested)
    {
      try
      {
        await _sendEmailsFromOutboxService.CheckForAndSendEmailsAsync();
      }
      catch(Exception ex)
      {
        _logger.Error(ex, "Error in {serviceName}", nameof(EmailSendingBackgroundService));
        await Task.Delay(delayMilliseconds, stoppingToken);
      }
      finally
      {
        await Task.Delay(delayMilliseconds, stoppingToken);
      }
    }
    _logger.Information("{serviceName} stopping", nameof(EmailSendingBackgroundService));
  }
}

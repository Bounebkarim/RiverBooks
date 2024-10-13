using System.Collections;
using Ardalis.Result;

namespace RiverBooks.EmailSending;

internal interface IOutboxService
{
  Task QueueEmailForSendingAsync(EmailOutboxEntity entity);
  Task<Result<List<EmailOutboxEntity>>> GetEmailsToBeSentAsync();
  Task MarkEmailAsSentAsync(EmailOutboxEntity email);
}

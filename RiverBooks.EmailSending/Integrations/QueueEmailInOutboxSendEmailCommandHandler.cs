using Ardalis.Result;
using MediatR;
using RiverBooks.EmailSending.Contracts;

namespace RiverBooks.EmailSending.Integrations;

internal class QueueEmailInOutboxSendEmailCommandHandler(IOutboxService outboxService)
  : IRequestHandler<SendEmailCommand, Result<Guid>>
{
  private readonly IOutboxService _outboxService = outboxService;

  public async Task<Result<Guid>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
  {
    var entity = new EmailOutboxEntity
    {
      To = request.To,
      From = request.From,
      Subject = request.Subject,
      Body = request.Body
    };
    await _outboxService.QueueEmailForSendingAsync(entity);

    return entity.Id;
  }
}

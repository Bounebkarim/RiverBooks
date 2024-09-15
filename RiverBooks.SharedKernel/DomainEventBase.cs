using MediatR;

namespace RiverBooks.SharedKernel;

public abstract class DomainEventBase : INotification
{
  public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;
}

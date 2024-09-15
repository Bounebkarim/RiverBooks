using MediatR;

namespace RiverBooks.SharedKernel;

public class MediatRDomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
  public async Task DispatchAndClearDomainEventsAsync(IHaveDomainEvents[] changedEntitiesWithEvents)
  {
    foreach (var entity in changedEntitiesWithEvents)
    {
      foreach (var domainEvent in entity.DomainEvents)
      {
        await mediator.Publish(domainEvent).ConfigureAwait(false);
      }
      entity.ClearDomainEvents();
    }
  }
}

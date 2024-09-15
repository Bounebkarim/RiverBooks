namespace RiverBooks.SharedKernel;

public interface IDomainEventDispatcher
{
  Task DispatchAndClearDomainEventsAsync(IHaveDomainEvents[] changedEntitiesWithEvents);
}

namespace RiverBooks.SharedKernel;

public interface IHaveDomainEvents
{
  public IEnumerable<DomainEventBase> DomainEvents { get; }
  void ClearDomainEvents();
}

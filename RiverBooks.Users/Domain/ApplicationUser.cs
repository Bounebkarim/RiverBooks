using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using RiverBooks.SharedKernel;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.Domain;

public class ApplicationUser : IdentityUser, IHaveDomainEvents
{
  public string FullName { get; set; } = string.Empty;

  private readonly List<CartItem> _cartItems = [];
  public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

  private readonly List<UserStreetAdress> _addresses = [];
  public IReadOnlyCollection<UserStreetAdress> Addresses => _addresses.AsReadOnly();

  private readonly List<DomainEventBase> _domainEvents = [];
  [NotMapped]
  public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();
  protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
  void IHaveDomainEvents.ClearDomainEvents() => _domainEvents.Clear();
  internal void AddCartItem(CartItem cartItem)
  {
    Guard.Against.Null(cartItem);
    var itemExist = _cartItems.SingleOrDefault(x => x.BookId == cartItem.BookId);
    if (itemExist != null)
    {
      itemExist.UpdateQuantity(cartItem.Quantity + itemExist.Quantity);
      itemExist.UpdatePrice(cartItem.UnitePrice);
      itemExist.UpdateDescription(cartItem.Description);
      return;
    }
    _cartItems.Add(cartItem);
  }

  internal void ClearCart()
  {
    _cartItems.Clear();
  }

  internal UserStreetAdress AddAddress(Address address)
  {
    Guard.Against.Null(address);
    var existingAddress = _addresses.SingleOrDefault(x => x.StreetAddress == address);
    if (existingAddress != null)
    {
      return existingAddress;
    }
    var userAddress = new UserStreetAdress(Id, address);
    _addresses.Add(userAddress);
    // Raise Domain Event address added
    var addressAddedEvent = new AddressAddedEvent(userAddress);
    RegisterDomainEvent(addressAddedEvent);

    return userAddress;
  }
}

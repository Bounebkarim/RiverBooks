using Ardalis.Result;
using MediatR;
using RiverBooks.Users.CartEndpoints;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.UseCases.Cart;

public record ListCartItemsQuery(string EmailAdress) : IRequest<Result<List<CartItemDto>>>;

public class ListCartItemsQueryHandler(IApplicationUserRepository userRepository) : IRequestHandler<ListCartItemsQuery, Result<List<CartItemDto>>>
{
  private readonly IApplicationUserRepository _userRepository = userRepository;

  public async Task<Result<List<CartItemDto>>> Handle(ListCartItemsQuery request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAdress);
    if (user is null)
    {
      return Result<List<CartItemDto>>.Unauthorized();
    }
    var cartItems = user.CartItems.Select(ci => new CartItemDto(ci.Id, ci.BookId, ci.Description, ci.UnitePrice, ci.Quantity)).ToList();
    return cartItems;
  }
}

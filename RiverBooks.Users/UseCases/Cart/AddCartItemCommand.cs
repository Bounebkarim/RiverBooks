using Ardalis.Result;
using FluentValidation;
using MediatR;
using RiverBooks.Books.Contracts;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.UseCases.Cart;

public record AddCartItemCommand(string? EmailAdress, Guid ReqBookId, int ReqQuantity) : IRequest<Result>;

public class AddCartItemCommandHandler(IApplicationUserRepository userRepository, IMediator mediator) : IRequestHandler<AddCartItemCommand, Result>
{
  private readonly IApplicationUserRepository _userRepository = userRepository;
  private readonly IMediator _mediator = mediator;

  public async Task<Result> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAdress);
    if (user is null)
    {
      return Result.Unauthorized();
    }
    var bookDetailsQuery = new BookDetailsQuery(request.ReqBookId);
    var bookDetailsResponse = await _mediator.Send(bookDetailsQuery, cancellationToken);
    if (bookDetailsResponse.Status == ResultStatus.NotFound)
    {
      return Result.NotFound();
    }
    var bookDetails = bookDetailsResponse.Value;

    var description = $"{bookDetails.Title} by {bookDetails.Author}";
    var newCartItem = new CartItem(request.ReqBookId
                                  , description
                                  , request.ReqQuantity
                                  , bookDetails.Price);

    user.AddCartItem(newCartItem);
    await _userRepository.SaveChangesAsync();
    return Result.Success();
  }
}

public class AddCartItemCommandValidator : AbstractValidator<AddCartItemCommand>
{
  public AddCartItemCommandValidator()
  {
    RuleFor(o => o.ReqQuantity).GreaterThan(0).WithMessage("Quantity must be more than 0.");
    RuleFor(o => o.EmailAdress).NotEmpty().WithMessage("Email must be provided.");
    RuleFor(o => o.ReqBookId).NotEmpty().WithMessage("BookId must be provided.");
  }
}

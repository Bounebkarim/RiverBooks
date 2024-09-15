using FastEndpoints;
using FluentValidation;

namespace RiverBooks.Books.Endpoints;

internal class UpdateBookPriceRequestValidator : Validator<UpdateBookPriceRequest>
{
  public UpdateBookPriceRequestValidator()
  {
    RuleFor(o => o.NewPrice)
      .GreaterThanOrEqualTo(0)
      .WithMessage("New price must be more than 0");
  }
}

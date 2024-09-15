using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases;
using RiverBooks.Users.UseCases.Cart;

namespace RiverBooks.Users.CartEndpoints;
public class AddItem(IMediator mediator ) : Endpoint<AddCartItemRequest>
{
  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    Post("/cart");
    Claims("email");
  }

  public override async Task HandleAsync(AddCartItemRequest req, CancellationToken ct)
  {
    var emailAdress = User.FindFirstValue("email");
    if (string.IsNullOrEmpty(emailAdress))
    {
      await SendForbiddenAsync(cancellation: ct);
      return;
    }
    var command = new AddCartItemCommand(emailAdress, req.BookId, req.Quantity);
    var result = await _mediator.Send(command, ct);
    if(result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(cancellation: ct);
      return;
    }

    if (result.Status == ResultStatus.Invalid)
    {
      var response = result.ToMinimalApiResult();
      await SendResultAsync(response);
    }
    await SendOkAsync(cancellation: ct);
  }
}

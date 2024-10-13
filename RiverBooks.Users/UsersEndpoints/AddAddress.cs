using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases.User;
using RiverBooks.Users.UseCases.User.AddAddress;

namespace RiverBooks.Users.UsersEndpoints;
public class AddAddress(IMediator mediator) : Endpoint<AddAddressRequest>
{
  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    Post("/users/addresses");
    Claims("email");
  }

  public override async Task HandleAsync(AddAddressRequest req, CancellationToken ct)
  {
    var email = User.FindFirstValue("email");
    if (email == null)
    {
      await SendUnauthorizedAsync(ct);
      return;
    }
    var command = new AddAddressCommand(email, req.Street1, req.Street2, req.City, req.State, req.ZipCode, req.Country);
    var result = await _mediator.Send(command, ct);
    if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(ct);
      return;
    }
    await SendOkAsync(ct);
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases.User;

namespace RiverBooks.Users.UsersEndpoints;
public class ListAddresses(IMediator mediator) :EndpointWithoutRequest<ListAddressesResponse>
{
  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    Get("/users/addresses");
    Claims("email");
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var email = User.FindFirstValue("email");
    if (email == null)
    {
      await SendUnauthorizedAsync(ct);
      return;
    }
    var querry = new ListAddressesQuery(email);
    var result = await _mediator.Send(querry, ct);
    if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(ct);
      return;
    }
    var response = new ListAddressesResponse { Addresses = result.Value };
    await SendAsync(response, cancellation: ct);
  }
}

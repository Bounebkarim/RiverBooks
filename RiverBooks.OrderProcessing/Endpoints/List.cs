using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.OrderProcessing.UseCases;

namespace RiverBooks.OrderProcessing.Endpoints;
internal class List(IMediator mediator) : EndpointWithoutRequest<ListOrdersForUserResponse>
{
  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    Get("/orders");
    Claims("email");
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var emailAdress = User.FindFirstValue("email");
    var query = new ListOrdersForUserQuery(emailAdress);
    var result = await _mediator.Send(query, ct);
    if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(cancellation: ct);
      return;
    }
    var response = new ListOrdersForUserResponse()
    {
      Orders = result.Value
    };
    await SendOkAsync(response, cancellation: ct);
  }
}


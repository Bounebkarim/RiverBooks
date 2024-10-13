using Ardalis.Result.AspNetCore;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases.User.Create;

namespace RiverBooks.Users.UsersEndpoints;
internal class Create(IMediator mediator) : Endpoint<CreateUserRequest>
{
  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    Post("/users");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
  {
    var cmd = new CreateUserCommand(req.Email, req.Password);
    var result = await _mediator.Send(cmd, ct);

    if (!result.IsSuccess)
    {
      await SendResultAsync(result.ToMinimalApiResult());
      return;
    }
    await SendOkAsync(ct);

  }
}

using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RiverBooks.EmailSending.Contracts;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.UseCases.User.Create;

internal class CreateUserCommandHandler(
  UserManager<ApplicationUser> userManager,
  IMediator mediator)
  : IRequestHandler<CreateUserCommand, Result>
{
  private readonly IMediator _mediator = mediator;

  public async Task<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken)
  {
    var newUser = new ApplicationUser
    {
      Email = command.Email,
      UserName = command.Email
    };

    var result = await userManager.CreateAsync(newUser, command.Password);

    if (!result.Succeeded)
    {
      string[] errors = result.Errors.Select(e => e.Description).ToArray();
      return Result.Error(new ErrorList(errors));
    }

    // send welcome email
    var sendEmailCommand = new SendEmailCommand
    {
      To = command.Email,
      From = "donotreply@test.com",
      Subject = "Welcome to RiverBooks!",
      Body = "Thank you for registering."
    };

    _ = await _mediator.Send(sendEmailCommand, cancellationToken);

    return Result.Success();
  }
}

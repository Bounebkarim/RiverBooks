using System.Net;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.UsersEndpoints;
internal class Create(UserManager<ApplicationUser> userManager) : Endpoint<CreateUserRequest>
{
  private readonly UserManager<ApplicationUser> _userManager = userManager;

  public override void Configure()
  {
    Post("/users");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
  {
    var newUser = new ApplicationUser
    {
       UserName = req.UserName,
      Email = req.Email
    };
    var result =await _userManager.CreateAsync(newUser,req.Password);
    if (!result.Succeeded)
    {
      AddError(string.Concat(result.Errors.Select(e => e.Description)),nameof( HttpStatusCode.BadRequest));
      await SendErrorsAsync(cancellation: ct);
      return;
    }

    await SendOkAsync(ct);

  }
}

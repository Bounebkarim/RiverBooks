using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.UsersEndpoints;

internal class Login(UserManager<ApplicationUser> userManager) : Endpoint<LoginRequest>
{
  private readonly UserManager<ApplicationUser> _userManager = userManager;
  public override void Configure()
  {
    Post("/users/login");
    AllowAnonymous();
  }

  public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
  {
    var user = await _userManager.FindByEmailAsync(req.Email);
    if (user == null)
    {
      await SendUnauthorizedAsync(ct);
      return;
    }
    var result = await _userManager.CheckPasswordAsync(user, req.Password);
    if (!result)
    {
      await SendUnauthorizedAsync(ct);
      return;
    }
    var jwtSecret = Config["Auth:Secret"];
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(jwtSecret!);
    var seckey = new SymmetricSecurityKey(key);
    var signingCreds = new SigningCredentials(seckey!, SecurityAlgorithms.HmacSha256Signature);
    var tokenInfo = tokenHandler.CreateToken(new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new Claim[]{ new (ClaimTypes.Name, user.Id) ,new(ClaimTypes.Email,user.Email!)}),
      SigningCredentials = signingCreds,
      Expires = DateTime.UtcNow.AddDays(7),
    });
    var token = tokenHandler.WriteToken(tokenInfo);

    await SendAsync(
      new
      {
        req.Email,
        user.UserName,
        Token = token
      }, cancellation: ct);
  }
}

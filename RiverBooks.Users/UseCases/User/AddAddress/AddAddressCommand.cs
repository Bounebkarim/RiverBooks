using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;
using Serilog;

namespace RiverBooks.Users.UseCases.User.AddAddress;

public record AddAddressCommand(string Email, string Street1, string Street2, string City, string State, string ZipCode, string Country) : IRequest<Result>;

public class AddAddressCommandHandler(IApplicationUserRepository userRepository, ILogger logger) : IRequestHandler<AddAddressCommand, Result>
{
  private readonly IApplicationUserRepository _userRepository = userRepository;
  private readonly ILogger _logger = logger;

  public async Task<Result> Handle(AddAddressCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithAddressesByEmailAsync(request.Email);
    if (user is null)
    {
      return Result.Unauthorized();
    }

    var adressToAdd = new Address(request.Street1, request.Street2, request.City, request.State, request.ZipCode,
      request.Country);

    var userAddress = user.AddAddress(adressToAdd);

    await _userRepository.SaveChangesAsync();

    _logger.Information("[UseCase] Added address {address} to user {email} (Total: {total})",
      userAddress.StreetAddress,
      request.Email,
      user.Addresses.Count);

    return Result.Success();

  }
}

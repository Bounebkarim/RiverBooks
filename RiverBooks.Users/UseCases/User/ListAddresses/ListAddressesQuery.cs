using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Interfaces;
using RiverBooks.Users.UsersEndpoints;

namespace RiverBooks.Users.UseCases.User.ListAddresses;

public record ListAddressesQuery(string Email) : IRequest<Result<List<UserAddressDto>>>;

public class ListAddressesHandler(IApplicationUserRepository userRepository)
  : IRequestHandler<ListAddressesQuery, Result<List<UserAddressDto>>>
{
  public async Task<Result<List<UserAddressDto>>> Handle(ListAddressesQuery request, CancellationToken cancellationToken)
  {
    var user = await userRepository.GetUserWithAddressesByEmailAsync(request.Email);
    if (user is null)
    {
      return Result<List<UserAddressDto>>.NotFound();
    }

    var response = user.Addresses
                                        .Select(a => new UserAddressDto(a.Id, a.StreetAddress.Street1,
                                        a.StreetAddress.Street2, a.StreetAddress.City, a.StreetAddress.State,
                                        a.StreetAddress.ZipCode, a.StreetAddress.Country)).ToList();

    return response.Count == 0 ? Result<List<UserAddressDto>>.NotFound() : Result<List<UserAddressDto>>.Success(response);
  }
}

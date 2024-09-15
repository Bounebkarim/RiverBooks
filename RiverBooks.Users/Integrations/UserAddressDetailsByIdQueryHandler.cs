using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Contracts;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.Integrations;
internal class UserAddressDetailsByIdQueryHandler(IReadOnlyUserStreetAddressRepository addressRepository) : IRequestHandler<UserAddressDetailsByIdQuery, Result<UserAddressDetails>>
{
  private readonly IReadOnlyUserStreetAddressRepository _addressRepository = addressRepository;

  public async Task<Result<UserAddressDetails>> Handle(UserAddressDetailsByIdQuery request, CancellationToken cancellationToken)
  {
    var address = await _addressRepository.GetByIdAsync(request.AddressId, cancellationToken);
    if (address is null)
    {
      return Result.NotFound();
    }

    var response = new UserAddressDetails(Guid.Parse(address.UserId), 
                                          address.Id, 
                                          address.StreetAddress.Street1,
                                          address.StreetAddress.Street2, 
                                          address.StreetAddress.City, 
                                          address.StreetAddress.State,
                                          address.StreetAddress.ZipCode, 
                                          address.StreetAddress.Country);
    return response;
  }
}

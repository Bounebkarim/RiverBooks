using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Interfaces;

internal interface IReadOnlyUserStreetAddressRepository
{
  Task<UserStreetAdress?> GetByIdAsync(Guid addressId, CancellationToken cancellationToken = default);
}

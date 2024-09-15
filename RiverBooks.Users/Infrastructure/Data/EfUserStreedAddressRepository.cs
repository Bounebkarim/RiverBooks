using Microsoft.EntityFrameworkCore;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.Infrastructure.Data;

internal class EfUserStreedAddressRepository(UsersDbContext dbContext) : IReadOnlyUserStreetAddressRepository
{
  private readonly UsersDbContext _dbContext = dbContext;

  public Task<UserStreetAdress?> GetByIdAsync(Guid addressId, CancellationToken cancellationToken = default)
  {
    return _dbContext.UserStreetAddresses
      //.Include(a => a.StreetAddress)
      .FirstOrDefaultAsync(a => a.Id == addressId, cancellationToken);
  }
}

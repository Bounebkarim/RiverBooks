using Microsoft.EntityFrameworkCore;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.Infrastructure.Data;

internal class EfApplicationUserRepository(UsersDbContext dbContext) : IApplicationUserRepository
{
  public async Task<ApplicationUser?> GetUserWithCartByEmailAsync(string? emailAdress)
  {
    return await dbContext.Users
      .Include(u => u.CartItems)
      .SingleOrDefaultAsync(u => u.Email == emailAdress);
  }

  public async Task SaveChangesAsync()
  {
    await dbContext.SaveChangesAsync();
  }

  public async Task<ApplicationUser?> GetUserWithAddressesByEmailAsync(string emailAdress)
  {
    return await dbContext.Users
      .Include(u => u.Addresses)
      .SingleOrDefaultAsync(u => u.Email == emailAdress);
  }

  public async Task<ApplicationUser?> GetByIdAsync(Guid id)
  {
    return await dbContext.Users
                          .SingleOrDefaultAsync(u => u.Id == id.ToString());
  }
}

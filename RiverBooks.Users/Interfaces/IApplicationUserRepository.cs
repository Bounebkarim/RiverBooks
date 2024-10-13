using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Interfaces;

public interface IApplicationUserRepository
{
  Task<ApplicationUser?> GetUserWithCartByEmailAsync(string? emailAdress);
  Task SaveChangesAsync();
  Task<ApplicationUser?> GetUserWithAddressesByEmailAsync(string emailAdress);

  Task<ApplicationUser?> GetByIdAsync(Guid id);
}

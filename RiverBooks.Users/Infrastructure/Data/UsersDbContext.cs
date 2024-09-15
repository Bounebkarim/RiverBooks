using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RiverBooks.SharedKernel;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.Infrastructure.Data;

public class UsersDbContext(DbContextOptions<UsersDbContext> options, IDomainEventDispatcher dispatcher) : DbContext(options)
{
  private readonly IDomainEventDispatcher? _dispatcher = dispatcher;
  public DbSet<ApplicationUser> Users { get; set; }
  public DbSet<UserStreetAdress> UserStreetAddresses { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasDefaultSchema("Users");
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    base.OnModelCreating(modelBuilder);
  }

  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    configurationBuilder.Properties<decimal>().HavePrecision(18, 6);
    configurationBuilder.Properties<string>().HaveMaxLength(100);
    base.ConfigureConventions(configurationBuilder);
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {

    var result = await base.SaveChangesAsync(cancellationToken);
    if (_dispatcher == null)
    {
      return result;
    }
    var changedEntitiesWithEvents = ChangeTracker.Entries<IHaveDomainEvents>()
      .Where(e => e.Entity.DomainEvents.Any())
      .Select(e => e.Entity)
      .ToArray();
    await _dispatcher.DispatchAndClearDomainEventsAsync(changedEntitiesWithEvents);
    return result;
  }
}

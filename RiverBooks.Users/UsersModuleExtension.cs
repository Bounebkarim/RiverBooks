using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.SharedKernel;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Infrastructure.Data;
using RiverBooks.Users.Interfaces;
using Serilog;

namespace RiverBooks.Users;
public static class UsersModuleExtension
{
  public static IServiceCollection AddUsersModule(this IServiceCollection services, ConfigurationManager config,
    ILogger logger, List<Assembly> mediatrAssemblies)
  {
    services.AddDbContext<UsersDbContext>(options =>
    {
      options.UseSqlServer(config.GetConnectionString("UsersConnectionString"));
    });

    //services.AddScoped<IUsersRepository, UsersRepository>();

    services.AddIdentityCore<ApplicationUser>()
      .AddEntityFrameworkStores<UsersDbContext>();
    services.AddScoped<IApplicationUserRepository, EfApplicationUserRepository>();
    services.AddScoped<IReadOnlyUserStreetAddressRepository, EfUserStreedAddressRepository>();
    mediatrAssemblies.Add(typeof(UsersModuleExtension).Assembly);
    logger.Information("{Module} : Module has been successfully registered ", "Users");
    return services;
  }
}

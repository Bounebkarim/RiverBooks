using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.OrderProcessing.Infrastructure;
using RiverBooks.OrderProcessing.Infrastructure.Data;
using RiverBooks.OrderProcessing.Interfaces;
using Serilog;

namespace RiverBooks.OrderProcessing;
public static class OrderProcessingModuleExtension
{
  public static IServiceCollection AddOrderProcessingModule(this IServiceCollection services, ConfigurationManager config,
    ILogger logger, List<Assembly> mediatrAssemblies)
  {
    services.AddDbContext<OrderProcessingDbContext>(options =>
    {
      options.UseSqlServer(config.GetConnectionString("OrderProcessingConnectionString"));
    });

    services.AddScoped<IOrderRepository, EfOrderRepository>();
    services.AddScoped<RedisOrderAddressCache>();
    services.AddScoped<IOrderAddressCache, ReadThroughOrderAddressCache>();
    mediatrAssemblies.Add(typeof(OrderProcessingModuleExtension).Assembly);
    logger.Information("{Module} : Module has been successfully registered ", "OrderProcessing");
    return services;
  }
}

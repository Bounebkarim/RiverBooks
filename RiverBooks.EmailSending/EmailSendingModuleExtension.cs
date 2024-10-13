using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Serilog;
using MongoDB.Driver;

namespace RiverBooks.EmailSending;

public static class EmailSendingModuleExtension
{
  public static IServiceCollection AddEmailSendingModule(this IServiceCollection services, ConfigurationManager config,
    ILogger logger, List<Assembly> mediatrAssemblies)
  {
    mediatrAssemblies.Add(typeof(EmailSendingModuleExtension).Assembly);
    services.Configure<MongoDbSettings>(config.GetSection("MongoDB"));
    services.AddMongoDB(config);
    services.AddTransient<ISendEmail, MimeKitEmailSender>();
    services.AddTransient<IOutboxService, MongoDbOutboxService>();
    services.AddTransient<ISendEmailsFromOutboxService, DefaultSendEmailsFromOutboxService>();
    services.AddHostedService<EmailSendingBackgroundService>();
    logger.Information("{Module} : Module has been successfully registered ", "EmailSending");
    return services;
  }

  public static IServiceCollection AddMongoDB(this IServiceCollection services,
    IConfiguration configuration)
  {
    // Register the MongoDB client as a singleton
    services.AddSingleton<IMongoClient>(serviceProvider =>
    {
      var settings = configuration.GetSection("MongoDB").Get<MongoDbSettings>();
      return new MongoClient(settings!.ConnectionString);
    });

    // Register the MongoDB database as a singleton
    services.AddSingleton(serviceProvider =>
    {
      var settings = configuration.GetSection("MongoDB").Get<MongoDbSettings>();
      var client = serviceProvider.GetService<IMongoClient>();
      return client!.GetDatabase(settings!.DatabaseName);
    });

    //// Optionally, register specific collections here as scoped or singleton services
    //// Example for a 'EmailOutboxEntity' collection
    services.AddTransient(serviceProvider =>
    {
      var database = serviceProvider.GetService<IMongoDatabase>();
      return database!.GetCollection<EmailOutboxEntity>("EmailOutboxEntityCollection");
    });

    return services;
  }
}

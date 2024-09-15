using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Books.Data;
using Serilog;

namespace RiverBooks.Books;

public static class BookServiceExtensions
{
  public static IServiceCollection AddBooksServices(this IServiceCollection services, ConfigurationManager config,
    ILogger logger, List<Assembly> mediatrAssemblies)
  {
    services.AddDbContext<BookDbContext>(options =>
    {
      options.UseSqlServer(config.GetConnectionString("BooksConnectionString"));
    });
    services.AddScoped<IBookService, BookService>();
    services.AddScoped<IBookRepository, EfBookRepository>();
    mediatrAssemblies.Add(typeof(BookServiceExtensions).Assembly);
    logger.Information("{Module} : Module has been successfully registered ", "Books");
    return services;
  }
}

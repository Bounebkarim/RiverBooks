using System.Reflection;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using RiverBooks.Books;
using RiverBooks.EmailSending;
using RiverBooks.OrderProcessing;
using RiverBooks.Reporting;
using RiverBooks.SharedKernel;
using RiverBooks.Users;
using RiverBooks.Users.UseCases.Cart;
using Serilog;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) =>
{
  config.ReadFrom.Configuration(builder.Configuration);
});
logger.Information("Application Starting");
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFastEndpoints()
  .AddAuthenticationJwtBearer(p => p.SigningKey = builder.Configuration["Auth:Secret"])
  .AddAuthorization()
  .SwaggerDocument();
List<Assembly> mediatrAssemblies = [typeof(RiverBooks.web.Program).Assembly];
//Add Module Services
builder.Services.AddBooksServices(builder.Configuration, logger, mediatrAssemblies);
builder.Services.AddEmailSendingModule(builder.Configuration, logger, mediatrAssemblies);
builder.Services.AddUsersModule(builder.Configuration, logger, mediatrAssemblies);
builder.Services.AddOrderProcessingModule(builder.Configuration, logger, mediatrAssemblies);
builder.Services.AddReportingModuleServices(builder.Configuration, logger, mediatrAssemblies);

//Set up MediatR
builder.Services.AddMediatR(cfg =>
                            cfg.RegisterServicesFromAssemblies(mediatrAssemblies.ToArray()));
builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
builder.Services.AddMediatRLoggingBehavior();
builder.Services.AddMediatRFluentValidationBehavior();
builder.Services.AddValidatorsFromAssemblyContaining<AddCartItemCommandValidator>();

var app = builder.Build();

app.UseFastEndpoints()
  .UseAuthentication()
  .UseAuthorization()
  .UseSwaggerGen();

app.Run();


namespace RiverBooks.web
{
  public partial class Program { }
} //for testing purposes

using FastEndpoints;
using Serilog;
using StackExchange.Redis;

namespace RiverBooks.OrderProcessing.Endpoints;
/// <summary>
/// Used to flush the cache and for testing purposes only 
/// </summary>
public class FlushCache : EndpointWithoutRequest
{
  private readonly ILogger _logger;
  private readonly IDatabase _db;

  public FlushCache(ILogger logger)
  {
    var redis = ConnectionMultiplexer.Connect("localhost");
    _db = redis.GetDatabase();
    _logger = logger;
  }

  public override void Configure()
  {
    Post("flushcache");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    await _db.ExecuteAsync("FLUSHDB");
    _logger.Information("Cache flushed for {db}", "REDIS");
  }
}

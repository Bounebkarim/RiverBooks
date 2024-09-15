using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.OrderProcessing.Interfaces;
using RiverBooks.Users.Contracts;
using Serilog;

namespace RiverBooks.OrderProcessing.Infrastructure;

internal class ReadThroughOrderAddressCache(RedisOrderAddressCache redisCache, IMediator mediator, ILogger logger) : IOrderAddressCache
{
  private readonly RedisOrderAddressCache _redisCache = redisCache;
  private readonly IMediator _mediator = mediator;
  private readonly ILogger _logger = logger;

  public async Task<Result<OrderAddress>> GetByIdAsync(Guid addressId, CancellationToken cancellationToken = default)
  {
    var result = await _redisCache.GetByIdAsync(addressId, cancellationToken);
    if (result.IsSuccess)
    {
      return result.Value;
    }
    if (result.Status == ResultStatus.NotFound)
    {
      _logger.Information("Address{id} not found; fetching from source", addressId);
      var query = new UserAddressDetailsByIdQuery(addressId);
      var queryResult = await _mediator.Send(query, cancellationToken);
      if (queryResult.IsSuccess)
      {
        var dto = queryResult.Value;
        var address = new Address(dto.Street1, dto.Street2, dto.City, dto.State, dto.ZipCode, dto.Country);
        var orderAddress = new OrderAddress(dto.Id, address);
        await StoreAsync(orderAddress, cancellationToken);
        return Result.Success(orderAddress);
      }
    }
    return Result.NotFound();
  }

  public Task StoreAsync(OrderAddress orderAdress, CancellationToken cancellationToken = default)
  {
    return _redisCache.StoreAsync(orderAdress, cancellationToken);
  }
}

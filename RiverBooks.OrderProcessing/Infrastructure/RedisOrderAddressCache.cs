﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ardalis.Result;
using Microsoft.Extensions.Logging;
using RiverBooks.OrderProcessing.Interfaces;
using StackExchange.Redis;

namespace RiverBooks.OrderProcessing.Infrastructure;

internal class RedisOrderAddressCache : IOrderAddressCache
{
  private readonly IDatabase _db;
  private readonly ILogger<RedisOrderAddressCache> _logger;

  public RedisOrderAddressCache(ILogger<RedisOrderAddressCache> logger)
  {
    var redis = ConnectionMultiplexer.Connect("localhost"); // TODO: Get server from config
    _db = redis.GetDatabase();
    _logger = logger;
  }

  public async Task<Result<OrderAddress>> GetByIdAsync(Guid id, CancellationToken ct)
  {
    string? fetchedJson = await _db.StringGetAsync(id.ToString());

    if (fetchedJson is null)
    {
      _logger.LogWarning("Address {id} not found in {db}", id, "REDIS");
      return Result.NotFound();
    }
    var address = JsonSerializer.Deserialize<OrderAddress>(fetchedJson);

    if (address is null) return Result.NotFound();

    _logger.LogInformation("Address {id} returned from {db}", id, "REDIS");
    return Result.Success(address);
  }

  public async Task StoreAsync(OrderAddress orderAddress, CancellationToken ct)
  {
    var key = orderAddress.Id.ToString();
    var addressJson = JsonSerializer.Serialize(orderAddress);

    await _db.StringSetAsync(key, addressJson);
    _logger.LogInformation("Address {id} stored in {db}", orderAddress.Id, "REDIS");
  }
}

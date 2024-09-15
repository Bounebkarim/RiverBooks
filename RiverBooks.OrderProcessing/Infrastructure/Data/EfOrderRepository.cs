﻿using Microsoft.EntityFrameworkCore;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.OrderProcessing.Interfaces;

namespace RiverBooks.OrderProcessing.Infrastructure.Data;

internal class EfOrderRepository(OrderProcessingDbContext dbContext) : IOrderRepository
{
  public async Task<List<Order>> ListAsync()
  {
    return await dbContext.Orders
                          .Include(o => o.OrderItems)
                          .ToListAsync();
  }

  public async Task AddAsync(Order order)
  {
    await dbContext.Orders.AddAsync(order);
  }

  public async Task SaveChangesAsync()
  {
    await dbContext.SaveChangesAsync();
  }
}

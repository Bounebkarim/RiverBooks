using Ardalis.Result;
using RiverBooks.OrderProcessing.Infrastructure;

namespace RiverBooks.OrderProcessing.Interfaces;

internal interface IOrderAddressCache
{
  Task<Result<OrderAddress>> GetByIdAsync(Guid adressId, CancellationToken cancellationToken = default!);
  Task StoreAsync(OrderAddress orderAdress, CancellationToken cancellationToken = default!);
}

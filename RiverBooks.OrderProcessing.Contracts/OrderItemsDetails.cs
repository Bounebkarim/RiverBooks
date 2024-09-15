namespace RiverBooks.OrderProcessing.Contracts;

public record OrderItemsDetails(Guid BookId, int Quantity, decimal UnitPrice, string Description);

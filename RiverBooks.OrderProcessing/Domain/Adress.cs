namespace RiverBooks.OrderProcessing.Domain;

internal record Address(string Street1, string Street2, string City, string State, string ZipCode, string Country)
{
  public string FullAdress => $"{Street1} {Street2} {City} {State} {ZipCode} {Country}";
};

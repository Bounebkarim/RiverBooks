using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiverBooks.OrderProcessing.Domain;

namespace RiverBooks.OrderProcessing.Infrastructure.Data;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.Property(x => x.Id).ValueGeneratedNever();
    builder.ComplexProperty(o => o.ShippingAdress, adress =>
    {
      adress.Property(a => a.Street1).HasMaxLength(Constants.StreetMaxlength);
      adress.Property(a => a.Street2).HasMaxLength(Constants.StreetMaxlength);
      adress.Property(a => a.City).HasMaxLength(Constants.CityMaxlength);
      adress.Property(a => a.State).HasMaxLength(Constants.StateMaxlength);
      adress.Property(a => a.ZipCode).HasMaxLength(Constants.ZipcodeMaxlength);
      adress.Property(a => a.Country).HasMaxLength(Constants.CountryMaxlength);
    });
    builder.ComplexProperty(o => o.BillingAdress, adress =>
    {
      adress.Property(a => a.Street1).HasMaxLength(Constants.StreetMaxlength);
      adress.Property(a => a.Street2).HasMaxLength(Constants.StreetMaxlength);
      adress.Property(a => a.City).HasMaxLength(Constants.CityMaxlength);
      adress.Property(a => a.State).HasMaxLength(Constants.StateMaxlength);
      adress.Property(a => a.ZipCode).HasMaxLength(Constants.ZipcodeMaxlength);
      adress.Property(a => a.Country).HasMaxLength(Constants.CountryMaxlength);
    });
  }

}

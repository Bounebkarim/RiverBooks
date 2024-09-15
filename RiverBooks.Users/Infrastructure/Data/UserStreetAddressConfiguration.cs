using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Infrastructure.Data;

public class UserStreetAddressConfiguration : IEntityTypeConfiguration<UserStreetAdress>
{
  public void Configure(EntityTypeBuilder<UserStreetAdress> builder)
  {
    builder.ToTable(nameof(UserStreetAdress));
    builder.Property(address => address.Id).ValueGeneratedNever();
    builder.ComplexProperty(x => x.StreetAddress);
  }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiverBooks.Books.Data;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
  internal static readonly Guid Book1Guid = new Guid("e3f0c672-4c8a-4d88-a379-9c55d7bb91a1");
  internal static readonly Guid Book2Guid = new Guid("b1a82ab8-76e4-4bb4-8b4f-7a1b9ef3340f");
  internal static readonly Guid Book3Guid = new Guid("c2a29c12-5c6f-4f13-a0d2-f0930c5e63e4");

  public void Configure(EntityTypeBuilder<Book> builder)
  {
    builder.HasKey(b => b.Id);
    builder.Property(b => b.Title).HasMaxLength(DataSchemaConstants.DefaultNameLength).IsRequired();
    builder.Property(b => b.Author).HasMaxLength(DataSchemaConstants.DefaultNameLength).IsRequired();
    builder.HasData(GetSampleBookData());
  }

  private IEnumerable<Book> GetSampleBookData()
  {
    var tolken = "J.R.R. Tolkien";
    yield return new Book(Book1Guid, "The Fellowship of the Ring", tolken, 9.99m);
    yield return new Book(Book2Guid, "The Two Towers", tolken, 9.99m);
    yield return new Book(Book3Guid, "The Return of the King", tolken, 9.99m);
  }
}

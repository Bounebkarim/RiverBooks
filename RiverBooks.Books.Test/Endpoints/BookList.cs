using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using RiverBooks.Books.Endpoints;

namespace RiverBooks.Books.Test.Endpoints;
public class BookList(Fixture fixture) : TestBase<Fixture>
{
  [Fact]
  public async Task ShouldReturnThreeBooksAsync()
  {
    var response = await fixture.Client.GETAsync<List, ListBooksResponse>();
    response.Response.EnsureSuccessStatusCode();
    response.Result.Books!.Count.Should().Be(3);
  }
}
public class BookGetById(Fixture fixture) : TestBase<Fixture>
{
  [Theory]
  [InlineData("e3f0c672-4c8a-4d88-a379-9c55d7bb91a1", "The Fellowship of the Ring")]
  public async Task ShouldReturnBookGivenIdAsync(string id,string expectedTitle)
  {
    var request = new GetByIdBookRequest { Id = Guid.Parse(id) };
    var response = await fixture.Client.GETAsync<Get,GetByIdBookRequest,GetByIdBookResponse >(request);
    response.Response.EnsureSuccessStatusCode();
    response.Result.Book.Title.Should().Be(expectedTitle);
  }
}

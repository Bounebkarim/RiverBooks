using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastEndpoints;
using MongoDB.Driver;

namespace RiverBooks.EmailSending.ListEmailsEndpoints;
internal class ListEmails(IMongoCollection<EmailOutboxEntity> collection) : EndpointWithoutRequest<ListEmailsResponse>
{
  private readonly IMongoCollection<EmailOutboxEntity> _collection = collection;

  public override void Configure()
  {
    Get("/emails");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var filter = Builders<EmailOutboxEntity>.Filter.Empty;
    var emails = await _collection.Find(filter).ToListAsync(cancellationToken: ct);
    var response = new ListEmailsResponse
    {
      Count = emails.Count,
      Emails = emails
    };
    Response = response;
  }
}

internal record ListEmailsResponse
{
  public int Count { get; set; }
  public List<EmailOutboxEntity> Emails { get; init; } = new();
}

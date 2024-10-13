using Ardalis.Result;
using MongoDB.Driver;

namespace RiverBooks.EmailSending;

internal class MongoDbOutboxService(IMongoCollection<EmailOutboxEntity> collection) : IOutboxService
{
  private readonly IMongoCollection<EmailOutboxEntity> _collection = collection;

  public async Task QueueEmailForSendingAsync(EmailOutboxEntity entity)
  {
    await _collection.InsertOneAsync(entity);
  }

  public async Task<Result<List<EmailOutboxEntity>>> GetEmailsToBeSentAsync()
  {
    var filter = Builders<EmailOutboxEntity>.Filter.Eq(e => e.DateTimeUtcProcessed, null);
    return await _collection.Find(filter).ToListAsync();
  }

  public async Task MarkEmailAsSentAsync(EmailOutboxEntity email)
  {
    email.DateTimeUtcProcessed = DateTime.UtcNow;
    await _collection.ReplaceOneAsync(e => e.Id == email.Id, email);
  }
}

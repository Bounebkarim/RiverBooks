using Ardalis.Result;
using MediatR;

namespace RiverBooks.Books.Contracts;

public record BookDetailsQuery(Guid Id) : IRequest<Result<BookDetailsResponse>>;

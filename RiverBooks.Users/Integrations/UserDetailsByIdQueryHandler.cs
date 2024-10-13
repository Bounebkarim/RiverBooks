using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Contracts;
using RiverBooks.Users.UseCases.User.GetById;

namespace RiverBooks.Users.Integrations;

internal class UserDetailsByIdQueryHandler(IMediator mediator) : IRequestHandler<UserDetailsByIdQuery, Result<UserDetails>>
{
  private readonly IMediator _mediator = mediator;

  public async Task<Result<UserDetails>> Handle(UserDetailsByIdQuery request, CancellationToken cancellationToken)
  {
    var query = new GetUserByIdQuery(request.UserId);

    var result = await _mediator.Send(query, cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      return Result.NotFound();
    }

    return result.Map(u => new UserDetails(u.Id, u.Email));
    
  }
}

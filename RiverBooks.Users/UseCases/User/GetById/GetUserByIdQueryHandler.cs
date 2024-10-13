using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.UseCases.User.GetById;

internal class GetUserByIdQueryHandler(IApplicationUserRepository userRepository)
  : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
  public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
  {
    var user = await userRepository.GetByIdAsync(request.UserId);
    if (user is null)
    {
      return Result.NotFound();
    }

    return new UserDto(Guid.Parse(user.Id), user.Email!);
  }
}

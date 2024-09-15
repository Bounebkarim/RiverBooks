namespace RiverBooks.Users.UsersEndpoints;

internal record CreateUserRequest(string Email, string UserName, string Password);

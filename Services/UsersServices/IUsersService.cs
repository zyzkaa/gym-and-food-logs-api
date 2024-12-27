using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;

namespace WebApp.Services.UsersServices;

public interface IUsersService
{
    UserResponseDto RegisterUser(CreateUserDto newUserDto);
    UserResponseDto LoginUser(LoginUserDto loginUserDto);
}
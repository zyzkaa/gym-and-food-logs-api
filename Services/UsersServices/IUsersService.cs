using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Services.UsersServices;

public interface IUsersService
{
    UserResponseDto RegisterUser(User newUser);
    Task<UserResponseDto> LoginUser(LoginUserDto loginUserDto);
    Task<UserResponseDto> LogoutUser();
    GetUserResponseDto GetUser();
    Task<UserResponseDto> ChangeUserParameters(UserParametersDto userParametersDto);
}
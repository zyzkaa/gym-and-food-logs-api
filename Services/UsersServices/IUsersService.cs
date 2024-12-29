using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;

namespace WebApp.Services.UsersServices;

public interface IUsersService
{
    UserResponseDto RegisterUser(CreateUserDto newUserDto);
    Task<UserResponseDto> LoginUser(LoginUserDto loginUserDto);
    Task<UserResponseDto> LogoutUser();
    Task<UserParametersResponseDto> ChangeUserParameters(UserParametersDto userParametersDto);
}
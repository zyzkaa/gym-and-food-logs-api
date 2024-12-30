using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Services.UsersServices;

namespace WebApp.Controllers;

[ApiController]
[Route("/user")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] CreateUserDto newUserDto)
    {
        var userResponseDto = _usersService.RegisterUser(newUserDto);
        return Ok(userResponseDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
    {
        var userResponseDto = await _usersService.LoginUser(loginUserDto);
        if (userResponseDto.Message is not null)
        {
            return BadRequest(userResponseDto.Message);
        }

        return Ok(userResponseDto);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> LogoutUser()
    {
        var userResponseDto = await _usersService.LogoutUser();
        return Ok(userResponseDto);
    }

    [HttpPut("change_parameters")]
    [Authorize]
    public async Task<IActionResult> ChangeUserParameters([FromBody] UserParametersDto userParametersDto)
    {
        var userParametersResponseDto = await _usersService.ChangeUserParameters(userParametersDto);
        if (userParametersResponseDto.Message is not null)
        {
            return Redirect("/user/login");
        }
        return Ok(userParametersResponseDto);
    }
}
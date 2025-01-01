using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Entities;
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
    public IActionResult RegisterUser([FromBody] User newUser)
    {
        var userResponseDto = _usersService.RegisterUser(newUser);
        return Ok(userResponseDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
    {
        var userResponseDto = await _usersService.LoginUser(loginUserDto);
        
        return userResponseDto.Message != null ? Ok(userResponseDto) : NotFound(userResponseDto.Message);
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
        return Ok(userParametersResponseDto);
    }

    [HttpGet]
    public IActionResult GetUser()
    {
        return Ok(_usersService.GetUser());
    }
}
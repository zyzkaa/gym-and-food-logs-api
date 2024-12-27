using Microsoft.AspNetCore.Http.HttpResults;
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
        var userDto = _usersService.RegisterUser(newUserDto);
        return Ok(userDto);
    }

    [HttpPost("login")]
    public IActionResult LoginUser([FromBody] LoginUserDto loginUserDto)
    {
        var userDto = _usersService.LoginUser(loginUserDto);
        if (userDto.Message is not null)
        {
            return BadRequest(userDto.Message);
        }

        return Ok(userDto);
    }
}
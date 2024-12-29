using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Services.UsersServices;

public class UsersService : IUsersService
{
    private readonly WebAppContext _dbContext;
    private readonly IHttpContextAccessor _httpContentAccessor;
    public UsersService(WebAppContext dbContext, IHttpContextAccessor httpContextAccessor)
    {   
        _dbContext = dbContext;
        _httpContentAccessor = httpContextAccessor;
    }
    
    public UserResponseDto RegisterUser(CreateUserDto newUserDto)
    {
        var newUser = new User(newUserDto.Username, newUserDto.Password,
            newUserDto.Weight, newUserDto.Height, newUserDto.Age, DateTime.Now);
        // try
        // {
        //     _dbContext.Users.Add(newUser);
        // }
        // catch (DbUpdateException e)
        // {
        //     return new UserResponseDto(e.Message);
        // }
        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();
        return new UserResponseDto(newUser.Id, newUser.Username);
    }

    public async Task<UserResponseDto> LoginUser(LoginUserDto loginUserDto)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Username == loginUserDto.Username);
        if (user == null)
        {
            return new UserResponseDto("username not found");
        }

        if (user.Password != loginUserDto.Password)
        {
            return new  UserResponseDto("wrong password");
        }
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };
        var identity = new ClaimsIdentity(claims, "authScheme");
        var principal = new ClaimsPrincipal(identity);
        await _httpContentAccessor.HttpContext.SignInAsync(principal);
        return new UserResponseDto(user.Id, user.Username);
    }

    public async Task<UserResponseDto> LogoutUser()
    {
        try
        {
            var currentUserId = _httpContentAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _httpContentAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new UserResponseDto("logged out");
        }
        catch (Exception e)
        {
            return new UserResponseDto("user not logged in");
        }
    }

    public async Task<UserParametersResponseDto> ChangeUserParameters(UserParametersDto userParametersDto)
    {
        var currentUserId = _httpContentAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == int.Parse(currentUserId));
        user.Age = userParametersDto.Age;
        user.Height = userParametersDto.Height;
        user.Weight = userParametersDto.Weight;
        await _dbContext.SaveChangesAsync();
        return new UserParametersResponseDto(user.Weight, user.Height, user.Age);
    }
}
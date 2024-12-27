using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Services.UsersServices;

public class UsersService : IUsersService
{
    private readonly WebAppContext _dbContext;

    public UsersService(WebAppContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public UserResponseDto RegisterUser(CreateUserDto newUserDto)
    {
        var newUser = new User(newUserDto.Username, newUserDto.Password,
            newUserDto.Weight, newUserDto.Height, newUserDto.Age, DateTime.Now);
        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();
        return new UserResponseDto(newUser.Id, newUser.Username);
    }

    public UserResponseDto LoginUser(LoginUserDto loginUserDto)
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
        return new UserResponseDto(user.Id, user.Username);
    }
}
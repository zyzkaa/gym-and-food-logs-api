using System.Security.Claims;
using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quartz;
using WebApp.DTO;
using WebApp.Entities;
using WebApp.Services.NotificationServices;

namespace WebApp.Services.UsersServices;

public class UsersService : IUsersService
{
    private readonly WebAppContext _dbContext;
    private readonly IHttpContextAccessor _httpContentAccessor;
    private readonly ISchedulerFactory _schedulerFactory;
    
    public UsersService(WebAppContext dbContext, IHttpContextAccessor httpContextAccessor, ISchedulerFactory schedulerFactory)
    {   
        _dbContext = dbContext;
        _httpContentAccessor = httpContextAccessor;
        _schedulerFactory = schedulerFactory;
    }

    public GetUserResponseDto GetUser()
    {
        var user = _dbContext.Users
            .Include(u => u.Trainings) 
            .ThenInclude(t => t.StrengthExercises) 
            .ThenInclude(e => e.StrengthExercise)
            .Include(u => u.Trainings) 
            .ThenInclude(t => t.CardioExercises) 
            .ThenInclude(e=>e.CardioExercise)
            .Include(u => u.MealPlans) 
            .ThenInclude(mp => mp.Meals)
            .ThenInclude(m => m.Ingredients)
            .ThenInclude(I =>I.Product )
            .FirstOrDefault(u => u.Id == int.Parse(_httpContentAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));
        
        var userDto = new GetUserResponseDto()
        {
            Id = user.Id,
            Username = user.Username,
            Weight = user.Weight,
            Height = user.Height,
            Age = user.Age,
            CreatedAt = user.CreatedAt,
            ModifiedAt = user.ModifiedAt,
            Trainings = user.Trainings?
                .ToDictionary(
                    t => t.Name,
                    t => t.StrengthExercises
                        .Select(se => se.StrengthExercise.Name)
                        .Concat(t.CardioExercises.Select(ce => ce.CardioExercise.Name))
                        .ToList()
                ),
                    
            MealPlans = user.MealPlans?.Select(mp =>
                mp.Meals.Select(m => $"{m.Name} (calories: {m.CalculatedCalories})").ToArray()
            ).ToList()
        };
        return userDto;
    }

    private UserResponseDto ParseUserToDto(User user)
    {
        return new UserResponseDto()
        {
            Id = user.Id,
            Username = user.Username,
            Weight = user.Weight,
            Height = user.Height,
            Age = user.Age
        };
    }

    private UserResponseDto ReturnMessage(string message)
    {
        return new UserResponseDto()
        {
            Message = message
        };
    }
    
    public async Task<UserResponseDto> RegisterUser(User newUser)
    {
        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();

        await LoginUser(new LoginUserDto(newUser.Username, newUser.Password));
        return ParseUserToDto(newUser);
    }

    public async Task<UserResponseDto> LoginUser(LoginUserDto loginUserDto)
    {
        if(_httpContentAccessor.HttpContext.User.Identity.IsAuthenticated) _httpContentAccessor.HttpContext.SignOutAsync();
        var user = _dbContext.Users.FirstOrDefault(u => u.Username == loginUserDto.Username);
        
        if (user == null)
        {
            return ReturnMessage("username not found");
        }

        if (user.Password != loginUserDto.Password)
        {
            return ReturnMessage("wrong password");
        }
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };
        var identity = new ClaimsIdentity(claims, "authScheme");
        var principal = new ClaimsPrincipal(identity);
        await _httpContentAccessor.HttpContext.SignInAsync(principal);
        
        return ParseUserToDto(user);

    }

    
    public async Task<UserResponseDto> LogoutUser()
    {
        var currentUserId = _httpContentAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        await _httpContentAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return ReturnMessage("logged out");
    }

    public async Task<UserResponseDto> ChangeUserParameters(UserParametersDto userParametersDto)
    {
        var currentUserId = _httpContentAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == int.Parse(currentUserId));
        
        user.Age = userParametersDto.Age;
        user.Height = userParametersDto.Height;
        user.Weight = userParametersDto.Weight;
        user.ModifiedAt = DateTime.Now;
        await _dbContext.SaveChangesAsync();
        
        return ParseUserToDto(user);
    }

    public void SetFcmToken(string token)
    {
        var currentUserId = _httpContentAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == int.Parse(currentUserId));
        user.FcmToken = token;
        _dbContext.SaveChanges();
    }

    public async Task AddReminders(RemindersDto remindersDto)
    {
        var userId = int.Parse(_httpContentAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _dbContext.Users
            .Include(u => u.ReminderNotifications)
            .FirstAsync(u => u.Id == userId);

        var reminder = user.ReminderNotifications ?? new ReminderNotifications();
        reminder.Time = remindersDto.Time;
        reminder.Days = remindersDto.Days.Select(d => (WeekDays)d).ToList();

        user.ReminderNotifications = reminder;
        await _dbContext.SaveChangesAsync();

        var scheduler = await _schedulerFactory.GetScheduler();
        var jobKey = new JobKey($"reminder-{userId}");

        await scheduler.DeleteJob(jobKey); 

        if (reminder.Days.Any())
        {
            var cronDays = string.Join(',', reminder.Days.Select(d => ((int)d == 0 ? 7 : (int)d)));
            var cron = $"0 {reminder.Time.Minute} {reminder.Time.Hour} ? * {cronDays}";
            
            var job = JobBuilder.Create<WorkoutReminderJob>()
                .WithIdentity(jobKey)
                .UsingJobData("token", user.FcmToken!)
                .UsingJobData("userId", userId)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"trigger-{userId}")
                .WithCronSchedule(cron)
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            Console.WriteLine($"[QUARTZ] Job {jobKey.Name} scheduled for cron {cron}");
        }
    }


}
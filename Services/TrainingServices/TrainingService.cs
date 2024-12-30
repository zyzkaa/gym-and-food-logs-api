using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Services.TrainingServices;

public class TrainingService : ITrainingService
{
    private readonly WebAppContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICurrentTrainingSerivce _currentTrainingService;

    public TrainingService(WebAppContext dbContext, IHttpContextAccessor httpContextAccessor, ICurrentTrainingSerivce currentTrainingService)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _currentTrainingService = currentTrainingService;
    }
        
    public async Task<TrainingResponseDto> AddTraining(TrainingDto trainingDto)
    {
        DateTime date = DateTime.Parse(_httpContextAccessor.HttpContext.Session.GetString("date"));
        date += new TimeSpan(trainingDto.StartHour, trainingDto.StartMinute, 0);
        TimeSpan duration = new TimeSpan(trainingDto.Hours, trainingDto.Minutes, 0);
        string currentUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == int.Parse(currentUser));
        
        var newTraining = new Training(trainingDto.Name, date, duration, user);
        
        _dbContext.Trainings.Add(newTraining);
        await _dbContext.SaveChangesAsync();
        
        return new TrainingResponseDto(newTraining.Id, newTraining.Name);
    }

    public Task<TrainingResponseDto> GetTrainings()
    {
        throw new NotImplementedException();
    }

    public Task<TrainingResponseDto> GetTrainingById(int trainingId)
    {
        throw new NotImplementedException();
    }
}
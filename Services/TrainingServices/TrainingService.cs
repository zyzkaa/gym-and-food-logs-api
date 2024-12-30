using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Services.TrainingServices;

public class TrainingService : ITrainingService
{
    private readonly WebAppContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TrainingService(WebAppContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
        
    public async Task<TrainingResponseDto> AddTraining(TrainingDto trainingDto)
    {
        TimeSpan duration = new TimeSpan(trainingDto.Hours, trainingDto.Minutes, 0);
        
        string currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == int.Parse(currentUserId));

        var newTraining = new Training(trainingDto.Name, trainingDto.date, duration, user);
        
        foreach (var strExercise in trainingDto.StrExercises)
        {
            StrengthExercise exercise = await _dbContext.StrengthExercises.FirstOrDefaultAsync(e => e.Name == strExercise.Name);
            
            var strExerciseInTraining = new StrExerciseInTraining
            {
                StrengthExercise = exercise,
            };

            foreach (var (set, index) in strExercise.Sets.Select((set, index) => (set, index)))
            {
                var strExerciseParameters = new StrExerciseParameters(index, set.Weight, set.Repetitions);
                strExerciseInTraining.StrExerciseParameters.Add(strExerciseParameters);
            }

            newTraining.StrExerciseInTrainings.Add(strExerciseInTraining);
        }
        
        foreach (var carExercise in trainingDto.CarExercises)
        {
            CardioExercise exercise = await _dbContext.CardioExercises.FirstOrDefaultAsync(e => e.Name == carExercise.Name);
            
            var carExerciseInTraining = new CarExerciseInTraining
            {
                CardioExercise = exercise,
            };

            foreach (var (set, index) in carExercise.Sets.Select((set, index) => (set, index)))
            {
                var carExerciseParameters = new CarExerciseParameters(index, set.Speed, set.Seconds);
                carExerciseInTraining.CarExerciseParameters.Add(carExerciseParameters);
            }

            newTraining.CarExerciseInTrainings.Add(carExerciseInTraining);
        }
        
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
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

    User getCurrentUser()
    {
        string currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        User currnetUser = _dbContext.Users.Find(int.Parse(currentUserId));
        return currnetUser;
    }
        
    public async Task<Training> AddTraining(TrainingDto trainingDto)
    {
        Training newTraining = new Training()
        {
            Name = trainingDto.Name,
            Date = trainingDto.Date,
            Duration = trainingDto.Duration,
            User = getCurrentUser()
        };
        
        foreach (var strExerciseInTraining in trainingDto.StrExercises)
        {
            StrExerciseInTraining newStrEx = new StrExerciseInTraining()
            {
                Set = strExerciseInTraining.Set,
                Weight = strExerciseInTraining.Weight,
                Repetitions = strExerciseInTraining.Repetitions,
                StrengthExercise = _dbContext.StrengthExercises.Find(strExerciseInTraining.ExerciseId)
            };
            newTraining.StrExercises.Add(newStrEx);
        }
        
        foreach (var trainingDtoCarExercise in trainingDto.CarExercises)
        {
            CarExerciseInTraining newCarEx = new CarExerciseInTraining()
            {
                Inteval = trainingDtoCarExercise.Interval,
                Speed = trainingDtoCarExercise.Speed,
                Time = trainingDtoCarExercise.Time,
                CardioExercise = _dbContext.CardioExercises.Find(trainingDtoCarExercise.ExerciseId)
            };
            newTraining.CarExercises.Add(newCarEx);
        }
        
        _dbContext.Trainings.Add(newTraining);
        _dbContext.SaveChanges();
        return newTraining;
    }

    public async Task<IEnumerable<Training>> GetTrainings()
    {
        return _dbContext.Trainings
            .Include(t => t.StrExercises)
            .ThenInclude(str => str.StrengthExercise)
            .Include(t => t.CarExercises)
            .ThenInclude(car => car.CardioExercise)
            .Where(t => t.User == getCurrentUser());
    }

    public async Task<Training> GetTrainingById(int trainingId)
    {
        // var training = await _dbContext.Trainings.FindAsync(trainingId);
        // return training.User == getCurrentUser() ? training : null;
        return await _dbContext.Trainings.FindAsync(trainingId);
    }

    public async Task<IEnumerable<Training>> GetTrainingsByDate(DateOnly date)
    {
        return _dbContext.Trainings
            .Include(t => t.StrExercises)
            .ThenInclude(str => str.StrengthExercise)
            .Include(t => t.CarExercises)
            .ThenInclude(car => car.CardioExercise)
            .Where(t => t.Date == date)
            .Where(t => t.User == getCurrentUser());
    }

    public async Task<StrengthExercise> GetStrengthExerciseById(int strExerciseId)
    {
        return await _dbContext.StrengthExercises
            .Include(e => e.Muscles)
            .FirstOrDefaultAsync(e => e.Id == strExerciseId);
    }

    public async Task<Training> DeleteTrainingById(int trainingId)
    {
        Training trainingToDelete = await _dbContext.Trainings.FindAsync(trainingId);
        _dbContext.Trainings.Remove(trainingToDelete);
        return trainingToDelete;
    }
}
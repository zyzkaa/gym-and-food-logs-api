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

    private User GetCurrentUser()
    {
        string currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        User currnetUser = _dbContext.Users.Find(int.Parse(currentUserId));
        return currnetUser;
    }

    private int GetCurrentUserId()
    {
        string currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return int.Parse(currentUserId);
    }

    Training ParseTrainingFromDto(TrainingDto dto)
    {
        Training newTraining = new Training()
        {
            Name = dto.Name,
            Date = dto.Date,
            Duration = dto.Duration,
            User = GetCurrentUser()
        };
        
        foreach (var strExerciseInTraining in dto.StrExercisesDto)
        {
            StrExerciseInTraining newStrEx = new StrExerciseInTraining()
            {
                StrengthExercise = _dbContext.StrengthExercises.Find(strExerciseInTraining.ExerciseId),
            };
            
            foreach (var strParams in strExerciseInTraining.StrParams)
            { 
                var newParams = new StrParams()
                {
                    Set = strParams.Set,
                    Weight = strParams.Weight,
                    Repetitions = strParams.Repetitions
                };
                newStrEx.StrParams.Add(newParams);
            }
            
            newTraining.StrExercises.Add(newStrEx);
        }
        
        foreach (var carExerciseInTraining in dto.CarExercisesDto)
        {
            CarExerciseInTraining newCarEx = new CarExerciseInTraining()
            {
                CardioExercise = _dbContext.CardioExercises.Find(carExerciseInTraining.ExerciseId)
            };

            foreach (var carParams in carExerciseInTraining.CarParams)
            {
                var newParams = new CarParams()
                {
                    Inteval = carParams.Interval,
                    Speed = carParams.Speed,
                    Time = carParams.Time
                };
                newCarEx.CarParams.Add(newParams);
            }
            
            newTraining.CarExercises.Add(newCarEx);
        }

        return newTraining;
    }
        
    public async Task<Training> AddTraining(TrainingDto trainingDto)
    {
        Training training = ParseTrainingFromDto(trainingDto);
        await _dbContext.Trainings.AddAsync(training);
        await _dbContext.SaveChangesAsync();
        return training;
    }

    private IQueryable<Training> GetTrainingSummary()
    {
        return _dbContext.Trainings
            .Include(t => t.User)
            .Include(t => t.StrExercises)
            .ThenInclude(e => e.StrengthExercise)
            .Include(t => t.CarExercises)
            .ThenInclude(e => e.CardioExercise);
    }

    public Task<List<Training>> GetTrainings()
    {
        return GetTrainingSummary()
            .Where(t => t.User.Id == GetCurrentUserId())
            .ToListAsync();
    }

    public async Task<List<Training>> GetTrainingsByDate(DateOnly date)
    {
        return await GetTrainingSummary()
            .Where(t => t.Date == date)
            .Where(t => t.User.Id == GetCurrentUserId())
            .ToListAsync();
    }

    public async Task<Training> DeleteTrainingById(int trainingId)
    {
        var training = await _dbContext.Trainings.FindAsync(trainingId)
            ?? throw new KeyNotFoundException("Training not found");

        if (training.User.Id == GetCurrentUserId())
        {
            _dbContext.Trainings.Remove(training);
            _dbContext.SaveChanges(); 
            return training;
        }

        return null;
    }
    
    public async Task<Training> GetTrainingById(int trainingId)
    {
        var training = await GetTrainingSummary()
                           .FirstOrDefaultAsync(t => t.Id == trainingId)
                       ?? throw new KeyNotFoundException("Training not found");
            
        return training.User.Id == GetCurrentUserId() ? training : null;
    }
    
    public async Task<List<Training>> GetTrainingsByStrExerciseId(int exerciseId)
    {
        var execise = await _dbContext.StrengthExercises.FindAsync(exerciseId)
            ?? throw new KeyNotFoundException("Exercise not found");
        
        return await GetTrainingSummary()
            .Where(t => t.StrExercises.Any(e => e.StrengthExercise.Id == exerciseId))
            .ToListAsync();
    }

    public async Task<List<Training>> GetTrainingsByCarExerciseId(int exerciseId)
    {
        var execise = await _dbContext.CardioExercises.FindAsync(exerciseId)
                      ?? throw new KeyNotFoundException("Exercise not found");
        
        return await GetTrainingSummary()
            .Where(t => t.CarExercises.Any(e => e.CardioExercise.Id == exerciseId))
            .ToListAsync();
    }
    
    private IQueryable<Training> GetTrainingWithDetailsQuery()
    {
        return _dbContext.Trainings
            .Include(t => t.CarExercises)
            .ThenInclude(e => e.CarParams)
            .Include(t => t.CarExercises)
            .ThenInclude(e => e.CardioExercise)
            .ThenInclude(e => e.Mets)
            .Include(t => t.StrExercises)
            .ThenInclude(e => e.StrParams)
            .Include(t => t.StrExercises)
            .ThenInclude(e => e.StrengthExercise)
            .ThenInclude(e => e.Muscles);
    }

    private async Task<TrainingResponseDto> ParseTrainingToDto(Training training)
    {
        var trainingResponse = new TrainingResponseDto()
        {
            Id = training.Id,
            Name = training.Name,
            Duration = training.Duration
        };

        foreach (var strExerciseInTraining in training.StrExercises)
        {
            var paramsList = new List<TrainingResponseDto.StrParamsResponseDto>();
            double volume = 0;
            
            foreach (var strParams in strExerciseInTraining.StrParams)
            {
                var newParams = new TrainingResponseDto.StrParamsResponseDto(strParams.Set, strParams.Weight,
                    strParams.Repetitions, strParams.Weight * strParams.Repetitions);
                
                paramsList.Add(newParams);
                volume += newParams.Volume;
            }

            var newExercise = new TrainingResponseDto.StrengthExerciseResponseDto(
                strExerciseInTraining.StrengthExercise, paramsList, volume);
            
            trainingResponse.StrExercisesResponseDto.Add(newExercise);
        }
        
        double totalCalories = 0;
        foreach (var carExerciseInTraining in training.CarExercises)
        {
            var paramsList = new List<TrainingResponseDto.CarParamsResponseDto>();
            double caloriesInExercise = 0;
            
            foreach (var carParams in carExerciseInTraining.CarParams)
            {
                var timeInHours = carParams.Time.TotalHours;
                var met = await _dbContext.Mets
                    .Where(m => m.cardioExercise == carExerciseInTraining.CardioExercise)
                    .Where(m => m.StartSpeed <= carParams.Speed)
                    .OrderByDescending(m => m.StartSpeed)
                    .FirstAsync();
                var calories = carParams.Speed * timeInHours * GetCurrentUser().Weight;

                var newParams = new TrainingResponseDto.CarParamsResponseDto(carParams.Inteval, 
                    carParams.Speed, carParams.Time, calories);
                
                paramsList.Add(newParams);
                caloriesInExercise += calories;
            }
            
            var newExercise = new TrainingResponseDto.CarExerciseResponseDto(carExerciseInTraining.CardioExercise,
                paramsList, caloriesInExercise);
            totalCalories += caloriesInExercise;
            trainingResponse.CarExercisesResponseDto.Add(newExercise);
        }
        
        trainingResponse.TotalCalories = totalCalories;

        return trainingResponse;
    }

    public async Task<TrainingResponseDto> GetTrainingWithDetails(int trainingId)
    {
        var training = await GetTrainingWithDetailsQuery()
            .FirstOrDefaultAsync(t => t.Id == trainingId)
            ?? throw new KeyNotFoundException("Training not found");
        
        return await ParseTrainingToDto(training);
    }


}
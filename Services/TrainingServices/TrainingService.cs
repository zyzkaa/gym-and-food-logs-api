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

    private bool CheckUser(Training training)
    {
        return training.User.Id == GetCurrentUserId() ? true : false;
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
        
        foreach (var strExerciseInTraining in dto.StrengthExercises)
        {
            StrengthExerciseInTraining newStrEx = new StrengthExerciseInTraining()
            {
                StrengthExercise = _dbContext.StrengthExercises.Find(strExerciseInTraining.ExerciseId),
                Params = new List<StrengthExerciseParams>()
            };
            
            foreach (var strParams in strExerciseInTraining.StrengthExerciseParams)
            { 
                var newParams = new StrengthExerciseParams()
                {
                    Set = strParams.Set,
                    Weight = strParams.Weight,
                    Repetitions = strParams.Repetitions
                };
                newStrEx.Params.Add(newParams);
            }
            
            newTraining.StrengthExercises.Add(newStrEx);
        }
        
        foreach (var carExerciseInTraining in dto.CardioExercises)
        {
            CardioExerciseInTraining newCarEx = new CardioExerciseInTraining()
            {
                CardioExercise = _dbContext.CardioExercises.Find(carExerciseInTraining.ExerciseId),
                Params = new List<CardioExerciseParams>()
            };

            foreach (var carParams in carExerciseInTraining.CardioExerciseParams)
            {
                var newParams = new CardioExerciseParams()
                {
                    Inteval = carParams.Interval,
                    Speed = carParams.Speed,
                    Time = carParams.Time
                };
                newCarEx.Params.Add(newParams);
            }
            
            newTraining.CardioExercises.Add(newCarEx);
        }

        return newTraining;
    }
    
    private async Task<TrainingResponseDto> ParseTrainingToDetailedDto(Training training)
    {
        var trainingResponse = new TrainingResponseDto()
        {
            Id = training.Id,
            Name = training.Name,
            Duration = training.Duration
        };

        foreach (var strExerciseInTraining in training.StrengthExercises)
        {
            var paramsList = new List<TrainingResponseDto.StrParamsResponseDto>();
            double volume = 0;
            
            foreach (var strParams in strExerciseInTraining.Params)
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
        foreach (var carExerciseInTraining in training.CardioExercises)
        {
            var paramsList = new List<TrainingResponseDto.CarParamsResponseDto>();
            double caloriesInExercise = 0;
            
            foreach (var carParams in carExerciseInTraining.Params)
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
        
    public async Task<TrainingResponseDto> AddTraining(TrainingDto trainingDto)
    {
        Training training = ParseTrainingFromDto(trainingDto);
        await _dbContext.Trainings.AddAsync(training);
        await _dbContext.SaveChangesAsync();
        return await ParseTrainingToDetailedDto(training);
    }

    private IQueryable<Training> GetTrainingSummary()
    {
        return _dbContext.Trainings
            .Include(t => t.User)
            .Include(t => t.StrengthExercises)
            .ThenInclude(e => e.StrengthExercise)
            .Include(t => t.CardioExercises)
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

        if (CheckUser(training))
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
            
        return CheckUser(training) ? training : null;
    }
    
    public async Task<List<Training>> GetTrainingsByStrengthExerciseId(int exerciseId)
    {
        var execise = await _dbContext.StrengthExercises.FindAsync(exerciseId)
            ?? throw new KeyNotFoundException("Exercise not found");
        
        return await GetTrainingSummary()
            .Where(t => t.StrengthExercises.Any(e => e.StrengthExercise.Id == exerciseId))
            .Where(t => t.User.Id == GetCurrentUserId())
            .ToListAsync();
    }

    public async Task<List<Training>> GetTrainingsByCardioExerciseId(int exerciseId)
    {
        var execise = await _dbContext.CardioExercises.FindAsync(exerciseId)
                      ?? throw new KeyNotFoundException("Exercise not found");
        
        return await GetTrainingSummary()
            .Where(t => t.CardioExercises.Any(e => e.CardioExercise.Id == exerciseId))
            .Where(t => t.User.Id == GetCurrentUserId())
            .ToListAsync();
    }
    
    private IQueryable<Training> GetTrainingWithDetailsQuery()
    {
        return _dbContext.Trainings
            .Include(t => t.User)
            .Include(t => t.CardioExercises)
            .ThenInclude(e => e.Params)
            .Include(t => t.CardioExercises)
            .ThenInclude(e => e.CardioExercise)
            .ThenInclude(e => e.Mets)
            .Include(t => t.StrengthExercises)
            .ThenInclude(e => e.Params)
            .Include(t => t.StrengthExercises)
            .ThenInclude(e => e.StrengthExercise)
            .ThenInclude(e => e.Muscles);
    }

    public async Task<TrainingResponseDto> GetTrainingWithDetails(int trainingId)
    {
        var training = await GetTrainingWithDetailsQuery()
            .FirstOrDefaultAsync(t => t.Id == trainingId)
            ?? throw new KeyNotFoundException("Training not found");
        
        return CheckUser(training) ? await ParseTrainingToDetailedDto(training) : null;
    }

    public async Task<List<Training>> GetTrainigsWithRecordsByStrengthExerciseId(int exerciseId)
    {
        return await _dbContext.Trainings
            .Include(t => t.User)
            .Include(t => t.StrengthExercises.Where(e => e.StrengthExercise.Id == exerciseId))
                .ThenInclude(e => e.Params)
            .Include(t => t.StrengthExercises)
                .ThenInclude(e => e.StrengthExercise)
            .Where(t => t.User.Id == GetCurrentUserId() && 
                        t.StrengthExercises.Any(e => e.StrengthExercise.Id == exerciseId))
            .ToListAsync();
    }
}
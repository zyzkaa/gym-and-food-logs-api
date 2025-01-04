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
                    Set = strParams.set,
                    Weight = strParams.weight,
                    Repetitions = strParams.repetitions
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
                    Inteval = carParams.interval,
                    Speed = carParams.speed,
                    Time = carParams.time
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
            .Include(t => t.StrExercises)
            .ThenInclude(e => e.StrengthExercise)
            .Include(t => t.CarExercises)
            .ThenInclude(e => e.CardioExercise);
    }

    public Task<List<Training>> GetTrainings()
    {
        return GetTrainingSummary()
            .Where(t => t.User == GetCurrentUser())
            .ToListAsync();
    }

    public async Task<Training> GetTrainingById(int trainingId)
    {
        // var training = await _dbContext.Trainings.FindAsync(trainingId);
        // return training.User == getCurrentUser() ? training : null;
        // return await _dbContext.Trainings
        //     .Include(t => t.StrExercises)
        //         .ThenInclude(e => e.StrengthExercise)
        //     .Include(t => t.StrExercises)
        //         .ThenInclude(e => e.StrParams)
        //     .Include(t => t.CarExercises)
        //         .ThenInclude(e => e.CardioExercise)
        //     .Include(t => t.CarExercises)
        //         .ThenInclude(e => e.CarParams)
        //     .FirstOrDefaultAsync(t => t.Id == trainingId);

        return await GetTrainingSummary()
            .FirstOrDefaultAsync(t => t.Id == trainingId)
            ?? throw new KeyNotFoundException("Training not found");
    }

    public async Task<List<Training>> GetTrainingsByDate(DateOnly date)
    {
        return await GetTrainingSummary()
            .Where(t => t.Date == date)
            .Where(t => t.User == GetCurrentUser())
            .ToListAsync();
    }

    private IQueryable<Training> GetTrainingWithDetails()
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

    public async Task<Training> DeleteTrainingById(int trainingId)
    {
        Training trainingToDelete = await GetTrainingWithDetails()
                                        .FirstOrDefaultAsync(t => t.Id == trainingId) 
                                    ?? throw new KeyNotFoundException("Training not found");
        
        foreach (var strExerciseInTraining in trainingToDelete.StrExercises)
        {
            foreach (var strParams in strExerciseInTraining.StrParams)
            {
                _dbContext.StrParams.Remove(strParams);
            }

            _dbContext.StrExerciseInTrainings.Remove(strExerciseInTraining);
        }
        
        foreach (var carExerciseInTraining in trainingToDelete.CarExercises)
        {
            foreach (var carParams in carExerciseInTraining.CarParams)
            {
                _dbContext.CarParams.Remove(carParams);
            }

            _dbContext.CarExercisesInTrainings.Remove(carExerciseInTraining);
        }
        
        _dbContext.Trainings.Remove(trainingToDelete);
        await _dbContext.SaveChangesAsync();
        return trainingToDelete;
    }
    
    public async Task<StrengthExercise> GetStrengthExerciseById(int strExerciseId)
    {
        return await _dbContext.StrengthExercises
            .Include(e => e.Muscles)
            .FirstOrDefaultAsync(e => e.Id == strExerciseId)
            ?? throw new KeyNotFoundException("Exercise not found");
    }

    public async Task<CardioExercise> GetCardioExerciseById(int carExerciseId)
    {
        return await _dbContext.CardioExercises
            .FirstOrDefaultAsync(e => e.Id == carExerciseId) 
               ?? throw new KeyNotFoundException("Exercise not found");
    }

    public Task<List<StrengthExercise>> GetStrengthExercises()
    {
        return _dbContext.StrengthExercises
            .Include(e => e.Muscles)
            .ToListAsync();
    }

    public Task<List<CardioExercise>> GetCardioExercises()
    {
        return _dbContext.CardioExercises.ToListAsync();
    }
    
    public async Task<List<StrengthExercise>> GetExercisesByMuscleId(int muscleId)
    {
        var muscle = await _dbContext.Muscles.FindAsync(muscleId)
            ?? throw new KeyNotFoundException("Muscle not found");
        
        return await _dbContext.StrengthExercises
            .Where(e => e.Muscles.Any(m => m.Id == muscleId))
            .ToListAsync();
    }
    
}
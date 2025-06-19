using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WebApp.Entities;

namespace WebApp.Services.ExerciseServices;

public class ExerciseService : IExerciseService
{
    private readonly WebAppContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ExerciseService(WebAppContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;

    }
    
    public async Task<StrengthExercise> GetStrengthExerciseById(int strExerciseId)
    {
        return await _dbContext.StrengthExercises
                   .Include(e => e.Muscles)
                   .FirstAsync(e => e.Id == strExerciseId)
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
            .Include(e => e.Muscles)
            .ToListAsync();
    }
    
    public async Task<List<CardioExercise>> GetCardioExercisesBySearch(string name)
    {
        var exercises = await _dbContext.CardioExercises
            .Where(e =>
                EF.Functions.Like(e.NameEn, $"%{name}%") ||
                EF.Functions.Like(e.NamePl, $"%{name}%"))
            .ToListAsync();
        
        return exercises;
    }
    

    public async Task<List<StrengthExercise>> GetStrExercisesBySearch(string name)
    {
        var exercises = await _dbContext.StrengthExercises
            .Where(e =>
                EF.Functions.Like(e.NameEn, $"%{name}%") ||
                EF.Functions.Like(e.NamePl, $"%{name}%"))
            .Include(e => e.Muscles)
            .ToListAsync();
        
        return exercises;
    }

    public async Task<List<Muscle>> GetAllMuscles()
    {
        return await _dbContext.Muscles.ToListAsync();
    }
    
    public async Task<List<StrengthExercise>> GetStrExercisesBySearchAndMuscle(string name, int id)
    {
        return await _dbContext.StrengthExercises
            .Where(e => e.Muscles.Any(m => m.Id == id))
            .Where(e =>
                EF.Functions.Like(e.NameEn, $"%{name}%") ||
                EF.Functions.Like(e.NamePl, $"%{name}%"))
            .Include(e => e.Muscles)
            .ToListAsync();
    }

    public async Task<List<StrengthExercise>> GetRecentStrExercises()
    {
        int currentUserId =
            int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        return await _dbContext.StrengthExercisesInTraining
            .Include(e => e.Training)
            .Include(e => e.StrengthExercise.Muscles)
            .Where(e => e.Training.User.Id == currentUserId)
            .OrderByDescending(e => e.Training.Date)
            .Select(e => e.StrengthExercise)
            .Distinct()
            .Take(7)
            .ToListAsync();
    }

}
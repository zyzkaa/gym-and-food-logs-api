using Microsoft.EntityFrameworkCore;
using WebApp.Entities;

namespace WebApp.Services.ExerciseServices;

public class ExerciseService : IExerciseService
{
    private readonly WebAppContext _dbContext;

    public ExerciseService(WebAppContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
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
            .ToListAsync();
    }
}
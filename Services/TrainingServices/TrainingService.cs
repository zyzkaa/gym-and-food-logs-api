using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WebApp.DTO;
using WebApp.Entities;
using WebApp.Mappers;

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
        int currentUserId = GetCurrentUserId();
        User currnetUser = _dbContext.Users.Find(currentUserId);
        return currnetUser;
    }

    private int GetCurrentUserId()
    {
        Console.Out.WriteLine("GET VURRENT USER ID");
        
        var httpContext = _httpContextAccessor.HttpContext 
                          ?? throw new Exception("HttpContext is null");

        var user = httpContext.User 
                   ?? throw new Exception("HttpContext.User is null");

        var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)
                      ?? throw new Exception("User has no NameIdentifier claim");
        
        return int.Parse(idClaim.Value);
        
    }

    private bool CheckUser(Training training)
    {
        return training.User.Id == GetCurrentUserId();
    }

    
    public async Task<TrainingResponseDtoRec> AddTraining(TrainingRequestDto trainingRequestDto)
    {
        var currentUser = GetCurrentUser();
        var strExercises = await _dbContext.StrengthExercises.ToListAsync();
        var carExercises = await _dbContext.CardioExercises
            .Include(e => e.Mets)
            .ToListAsync();
        
        Training training = trainingRequestDto.FromDto(currentUser, carExercises, strExercises);
        await _dbContext.Trainings.AddAsync(training);
        await _dbContext.SaveChangesAsync();
        return training.ToResponseDto(currentUser); 
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

    public Task<List<Training>> GetTrainings(int page, int amount)
    {
        var currentUserId = GetCurrentUserId();
        
        return GetTrainingSummary()
            .Where(t => t.User.Id == currentUserId)
            .OrderByDescending(t => t.Date)
            .Skip(amount * (page - 1))
            .Take(amount)
            .ToListAsync();
    }

    public async Task<List<Training>> GetTrainingsByDate(DateOnly date)
    {
        var currentUserId = GetCurrentUserId();

        return await GetTrainingSummary()
            .Where(t => t.Date == date)
            .Where(t => t.User.Id == currentUserId)
            .ToListAsync();
    }

    public async Task<Training> DeleteTrainingById(int trainingId)
    {
        var training = await _dbContext.Trainings
                           .Include(t => t.User)
                           .Where(t => t.Id == trainingId)
                           .FirstAsync()
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
        var currentUserId = GetCurrentUserId();

        var execise = await _dbContext.StrengthExercises.FindAsync(exerciseId)
            ?? throw new KeyNotFoundException("Exercise not found");
        
        return await GetTrainingSummary()
            .Where(t => t.StrengthExercises.Any(e => e.StrengthExercise.Id == exerciseId))
            .Where(t => t.User.Id == currentUserId)
            .ToListAsync();
    }

    public async Task<List<Training>> GetTrainingsByCardioExerciseId(int exerciseId)
    {
        var currentUserId = GetCurrentUserId();

        var execise = await _dbContext.CardioExercises.FindAsync(exerciseId)
                      ?? throw new KeyNotFoundException("Exercise not found");
        
        return await GetTrainingSummary()
            .Where(t => t.CardioExercises.Any(e => e.CardioExercise.Id == exerciseId))
            .Where(t => t.User.Id == currentUserId)
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

    public async Task<TrainingResponseDtoRec?> GetTrainingWithDetails(int trainingId)
    {
        var currentUser = GetCurrentUser();

        var training = await GetTrainingWithDetailsQuery()
            .FirstOrDefaultAsync(t => t.Id == trainingId)
            ?? throw new KeyNotFoundException("Training not found");
        
        return CheckUser(training) ? training.ToResponseDto(currentUser) : null;
    }

    public async Task<List<Training>> GetTrainigsWithRecordsByStrengthExerciseId(int exerciseId)
    {
        var currentUserId = GetCurrentUserId();
        
        return await _dbContext.Trainings
            .Include(t => t.User)
            .Include(t => t.StrengthExercises.Where(e => e.StrengthExercise.Id == exerciseId))
                .ThenInclude(e => e.Params)
            .Include(t => t.StrengthExercises)
                .ThenInclude(e => e.StrengthExercise)
            .Where(t => t.User.Id == currentUserId && 
                        t.StrengthExercises.Any(e => e.StrengthExercise.Id == exerciseId))
            .ToListAsync();
    }
}
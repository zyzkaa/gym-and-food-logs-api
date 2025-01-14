using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WebApp.DTO;
using WebApp.DTO.Meals;
using WebApp.Entities;

namespace WebApp.Services.MealPlanServices;

public class MealPlanService : IMealPlanService
{
    private readonly WebAppContext _dbContext;
    private readonly IHttpContextAccessor _httpContentAccessor;

    public MealPlanService(WebAppContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContentAccessor = httpContextAccessor;
    }
    

    public async Task<MealPlan> AddMealPlan(MealPlanDto mealPlanDto)
    {
        // Pobranie ID zalogowanego użytkownika z kontekstu
        var userIdClaim = _httpContentAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            throw new UnauthorizedAccessException("User is not authenticated");

        int userId = int.Parse(userIdClaim.Value);

        // Pobranie użytkownika z bazy
        var user = await _dbContext.Users
            .Include(u => u.MealPlans) // Załaduj istniejące MealPlans
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            throw new KeyNotFoundException($"User with ID {userId} not found");

        // Pobranie posiłków w jednym zapytaniu
        var meals = await _dbContext.Meals
            .Where(m => mealPlanDto.MealsID.Contains(m.Id))
            .ToListAsync();

        if (meals.Count != mealPlanDto.MealsID.Count)
            throw new KeyNotFoundException("One or more meals not found");

        // Tworzenie MealPlan
        var mealPlan = new MealPlan
        {
            User = user,
            Date = mealPlanDto.Date,
            Meals = meals // Przypisanie posiłków
        };

        // Dodanie MealPlan do kolekcji użytkownika
        user.MealPlans.Add(mealPlan);

        // Dodanie MealPlan do bazy
        await _dbContext.MealPlans.AddAsync(mealPlan);
        await _dbContext.SaveChangesAsync();
        return mealPlan;
    }



    public async Task<List<MealPlan>> GetMealPlans()
    {
        return await _dbContext.MealPlans
            .Include(mp => mp.Meals)
            .ThenInclude(m => m.Ingredients)
            .ThenInclude(i => i.Product)
            .ToListAsync();
    }

    public async Task<MealPlan> GetMealPlanById(int planId)
    {
        return await _dbContext.MealPlans
                   .Include(mp => mp.Meals)
                   .ThenInclude(m => m.Ingredients)
                   .ThenInclude(i => i.Product)
                   .FirstOrDefaultAsync(mp => mp.Id == planId)
               ?? throw new KeyNotFoundException("Meal Plan not found");
    }

    public async Task<List<Meal>> GetMealPlanByCalories(int calories)
    {
        // Pobierz wszystkie posiłki z bazy
        var allMeals = await _dbContext.Meals.ToListAsync();

        // Przefiltruj posiłki, które mają mniej kalorii niż `targetCalories`
        var filteredMeals = allMeals.Where(m => m.CalculatedCalories <= calories).ToList();

        if (filteredMeals.Count < 3)
        {
            throw new Exception("Not enough meals available to create a combination");
        }

        var random = new Random();

        // Losowo wybierz 3 różne posiłki
        List<Meal> result = null;
        int bestDifference = int.MaxValue;

        for (int i = 0; i < 100; i++) // Powtórz losowanie do 100 razy, aby znaleźć najlepszą kombinację
        {
            var selectedMeals = filteredMeals.OrderBy(x => random.Next()).Take(3).ToList();
            int totalCalories = selectedMeals.Sum(m => (int)m.CalculatedCalories);

            // Jeśli różnica między `totalCalories` a `targetCalories` jest mniejsza, zapisz tę kombinację
            int difference = Math.Abs(calories - totalCalories);
            if (difference < bestDifference)
            {
                bestDifference = difference;
                result = selectedMeals;
            }

            // Jeśli znaleziono idealną kombinację, przerwij
            if (bestDifference == 0)
                break;
        }

        return result;
    }

    public async Task<MealPlan> DeleteMealPlanById(int planId)
    {
        var mealPlan = await _dbContext.MealPlans.FindAsync(planId)
                       ?? throw new KeyNotFoundException("Meal Plan not found");
        _dbContext.MealPlans.Remove(mealPlan);
        await _dbContext.SaveChangesAsync();
        return mealPlan;
    }
}
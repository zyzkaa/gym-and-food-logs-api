using Microsoft.EntityFrameworkCore;
using WebApp.DTO;
using WebApp.DTO.Meals;
using WebApp.Entities;

namespace WebApp.Services.MealPlanServices;

public class MealPlanService : IMealPlanService
{
    private readonly WebAppContext _dbContext;

    public MealPlanService(WebAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MealPlan> AddMealPlan(MealPlanDto mealPlanDto)
    {
        // Pobranie użytkownika
        var user = await _dbContext.Users.FindAsync(mealPlanDto.UserId);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {mealPlanDto.UserId} not found");

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

    public async Task<MealPlan> DeleteMealPlanById(int planId)
    {
        var mealPlan = await _dbContext.MealPlans.FindAsync(planId)
                       ?? throw new KeyNotFoundException("Meal Plan not found");
        _dbContext.MealPlans.Remove(mealPlan);
        await _dbContext.SaveChangesAsync();
        return mealPlan;
    }
}
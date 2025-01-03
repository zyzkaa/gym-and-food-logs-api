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
        var user = await _dbContext.Users.FindAsync(mealPlanDto.UserId)
                   ?? throw new KeyNotFoundException("User not found");

        var mealPlan = new MealPlan
        {
            Date = mealPlanDto.Date,
            User = user,
            Meals = mealPlanDto.Meals.Select(m => _dbContext.Meals.Find(m.Id)).ToList()
        };

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
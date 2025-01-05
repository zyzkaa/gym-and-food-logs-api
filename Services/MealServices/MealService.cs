using Microsoft.EntityFrameworkCore;
using WebApp.DTO;
using WebApp.DTO.Meals;
using WebApp.Entities;

namespace WebApp.Services.MealServices;

public class MealService : IMealService
{
    private readonly WebAppContext _dbContext;

    public MealService(WebAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Meal> AddMeal(MealDto mealDto)
    {
        var meal = new Meal
        {
            Name = mealDto.Name
        };
        meal.Ingredients = mealDto.Ingredients.Select(i => new MealIngredient
        {
            Quantity = i.Quantity,
            Product = _dbContext.Products.FirstOrDefault(p => p.Id == i.ProductId),
            Meal = meal,
        }).ToList();

        await _dbContext.Meals.AddAsync(meal);
        await _dbContext.SaveChangesAsync();
        return meal;
    }

    public async Task<List<Meal>> GetMeals()
    {
        return await _dbContext.Meals
            .Include(m => m.Ingredients)
            .ThenInclude(i => i.Product)
            .ToListAsync();
    }

    public async Task<Meal> GetMealById(int mealId)
    {
        return await _dbContext.Meals
                   .Include(m => m.Ingredients)
                   .ThenInclude(i => i.Product)
                   .FirstOrDefaultAsync(m => m.Id == mealId)
               ?? throw new KeyNotFoundException("Meal not found");
    }

    public async Task<Meal> DeleteMealById(int mealId)
    {
        var meal = await _dbContext.Meals.FindAsync(mealId)
                   ?? throw new KeyNotFoundException("Meal not found");
        _dbContext.Meals.Remove(meal);
        await _dbContext.SaveChangesAsync();
        return meal;
    }
}
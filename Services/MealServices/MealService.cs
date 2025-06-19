using System.Security.Claims;
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

    public async Task<Meal> AddMeal(MealDto mealDto,int userId)
    {
        var meal = new Meal
        {
            Name = mealDto.Name
        };
        meal.Description = mealDto.Description;
        meal.IsShared = mealDto.IsShared;
        meal.OriginalMealID = mealDto.OriginalMealID;
        meal.EditorID = userId;
        meal.ImageURL = mealDto.ImageURL;
        meal.CreatorID = mealDto.CreatorId;
        meal.CreatedAt = DateTime.Now;
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
            .Where(m => m.IsShared)
            .ToListAsync();
    }
    public async Task<List<Meal>> GetMostRecentMeals(int count)
    {
        return await _dbContext.Meals
            .Include(m => m.Ingredients)
            .ThenInclude(i => i.Product)
            .Where(m => m.IsShared)
            .OrderByDescending(m => m.CreatedAt)
            .Take(count)
            .ToListAsync();
    }

    public async Task<NutritionTotalsDto> CalculateTotalsAsync(List<IngredientInputDto> ingredients)
    {
        var result = new NutritionTotalsDto();

        foreach (var ingredient in ingredients)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == ingredient.ProductId); // Replace with your actual repo call

            if (product == null)
                continue;

            double scale = ingredient.Quantity / 100;

            result.TotalCalories += product.CaloriesPer100g * scale;
            result.Protein       += product.ProteinPer100g  * scale;
            result.Carbs         += product.CarbsPer100g    * scale;
            result.Fat           += product.FatPer100g      * scale;
        }

        return result;
    }

    public async Task<List<Meal>> GetMyEditedMeals(int userId)
    {
        return await _dbContext.Meals
                   .Include(m => m.Ingredients)
                   .ThenInclude(i => i.Product)
                   .Where(m => m.EditorID == userId).ToListAsync()
               ?? throw new KeyNotFoundException("Meal not found");
    }


    public async Task<Meal> GetMealById(int mealId)
    {
        return await _dbContext.Meals
                   .Include(m => m.Ingredients)
                   .ThenInclude(i => i.Product)
                   .FirstOrDefaultAsync(m => m.Id == mealId)
               ?? throw new KeyNotFoundException("Meal not found");
    }
    public async Task<List<Meal>> GetMealsByUser(int userId)
    {
        return await _dbContext.Meals
                   .Include(m => m.Ingredients)
                   .ThenInclude(i => i.Product)
                   .Where(m => m.IsShared).ToListAsync()
               ?? throw new KeyNotFoundException("Meal not found");
    }
    public async Task<List<Meal>> GetMyMeals(int userId)
    {
        return await _dbContext.Meals
                   .Include(m => m.Ingredients)
                   .ThenInclude(i => i.Product)
                   .Where(m => m.CreatorID == userId).ToListAsync()
               ?? throw new KeyNotFoundException("Meal not found");
    }

    public async Task<List<Meal>> GetMealByProductId(int productId)
    {
        return await _dbContext.Meals
            .Include(m => m.Ingredients)
            .ThenInclude(I => I.Product)
            .Where(m => m.IsShared)
            .Where(m => m.Ingredients.Any(i => i.Product.Id == productId))
            .ToListAsync();
    }
    public async Task<Meal> DeleteMealById(int mealId)
    {
        var meal = await _dbContext.Meals
            .Include(m => m.Ingredients)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(m => m.Id == mealId)
                   ?? throw new KeyNotFoundException("Meal not found");
        _dbContext.Meals.Remove(meal);
        await _dbContext.SaveChangesAsync();
        return meal;
    }
}
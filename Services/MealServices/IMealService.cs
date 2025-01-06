using WebApp.DTO;
using WebApp.DTO.Meals;
using WebApp.Entities;

namespace WebApp.Services.MealServices;

public interface IMealService
{
    Task<Meal> AddMeal(MealDto mealDto);
    Task<List<Meal>> GetMeals();
    Task<Meal> GetMealById(int mealId);
    Task<List<Meal>> GetMealByProductId(int productId);
    Task<Meal> DeleteMealById(int mealId);
}
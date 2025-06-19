using WebApp.DTO;
using WebApp.DTO.Meals;
using WebApp.Entities;

namespace WebApp.Services.MealServices;

public interface IMealService
{
    Task<Meal> AddMeal(MealDto mealDto,int userId);
    Task<List<Meal>> GetMeals();
    Task<Meal> GetMealById(int mealId);
    Task<List<Meal>> GetMealsByUser(int userId);
    Task<List<Meal>> GetMyMeals(int userId);
    Task<List<Meal>> GetMealByProductId(int productId);
    Task<Meal> DeleteMealById(int mealId);
    Task<List<Meal>> GetMostRecentMeals(int count);
    Task<NutritionTotalsDto> CalculateTotalsAsync(List<IngredientInputDto> ingredientReturnDto);
    Task<List<Meal>> GetMyEditedMeals(int userId);

}
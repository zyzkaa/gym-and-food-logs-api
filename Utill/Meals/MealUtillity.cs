using WebApp.DTO.Meals;
using WebApp.Entities;

namespace WebApp.Utill.Meals;

public static class MealUtillity
{
    public static MealReturnDto ParseToMealDto(Meal meal)
    {
        MealReturnDto mealReturnDto = new MealReturnDto()
        {
            Name = meal.Name,
            Calories = meal.CalculatedCalories.ToString(),
            Ingredients = meal.Ingredients.Select(I => 
                I.Product.Name).ToList()
        };
        return mealReturnDto;
    }
}
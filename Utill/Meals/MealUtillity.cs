using WebApp.DTO.Meals;
using WebApp.Entities;

namespace WebApp.Utill.Meals;

public static class MealUtillity
{
    public static MealReturnDto ParseToMealDto(Meal meal)
    {
        MealReturnDto mealReturnDto = new MealReturnDto()
        {
            Id = meal.Id,
            Name = meal.Name,
            Calories = meal.CalculatedCalories,
            CreatorID = meal.CreatorID,
            CreatedAt = meal.CreatedAt,
            Ingredients = meal.Ingredients
                .Select(i => new IngredientReturnDTO
                {
                    Id = i.Product.Id,
                    Name = i.Product.Name
                }).ToList()

        };
        return mealReturnDto;
    }
}
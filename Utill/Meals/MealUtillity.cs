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
            Calories =double.Round(meal.CalculatedCalories,2),
            Protein = double.Round(meal.CalculatedProtein,2),
            Carbs = double.Round(meal.CalculatedCarbs,2),
            Fat = double.Round(meal.CalculatedFat,2),
            CreatorID = meal.CreatorID,
            EditorID = meal.EditorID,
            CreatedAt = meal.CreatedAt,
            ImageURL = meal.ImageURL,
            Description = meal.Description,
            OriginalMealID = meal.OriginalMealID,
            Ingredients = meal.Ingredients
                .Select(i => new IngredientReturnDTO
                {
                    Id = i.Product.Id,
                    Name = i.Product.Name,
                    Quantity = i.Quantity
                }).ToList()

        };
        return mealReturnDto;
    }
}
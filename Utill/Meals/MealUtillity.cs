using WebApp.DTO.Meals;
using WebApp.Entities;

namespace WebApp.Utill.Meals;

public static class MealUtillity
{
    public static MealDto ParseToMealDto(Meal meal)
    {
        MealDto mealDto = new MealDto()
        {
            Id = meal.Id,
            Name = meal.Name,
            Ingredients = meal.Ingredients.Select(I => new MealIngredientDto
            {
                ProductId = I.Product.Id,
                Quantity = I.Quantity
            }).ToList()
        };
        return mealDto;
    }
}
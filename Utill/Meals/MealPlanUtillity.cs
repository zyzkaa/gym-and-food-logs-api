using WebApp.DTO.Meals;
using WebApp.Entities;

namespace WebApp.Utill.Meals;

public static class MealPlanUtillity
{
    public static MealPlanReturnDto ParseFullMealPlan(MealPlan mealPlan)
    {
        return new MealPlanReturnDto()
        {
            Id = mealPlan.Id,
            Date = mealPlan.Date,
            UserName = mealPlan.User?.Username ?? "", // Jeśli mp.User == null, ustaw domyślną wartość 0
            MealsName = mealPlan.Meals?.Select(m => m.Name).ToList() ??
                        new List<string>() // Jeśli mp.Meals == null, ustaw pustą listę
        };
    }
}
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
            UserName = mealPlan.User?.Username ?? "",
            MealsName = mealPlan.Meals?.Select(m => m.Name).ToList() ??
                        new List<string>() 
        };
    }
}
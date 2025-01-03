using WebApp.DTO;
using WebApp.DTO.Meals;
using WebApp.Entities;

namespace WebApp.Services.MealPlanServices;

public interface IMealPlanService
{
    Task<MealPlan> AddMealPlan(MealPlanDto mealPlanDto);
    Task<List<MealPlan>> GetMealPlans();
    Task<MealPlan> GetMealPlanById(int planId);
    Task<MealPlan> DeleteMealPlanById(int planId);
}
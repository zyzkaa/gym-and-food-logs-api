using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.DTO.Meals;
using WebApp.Entities;
using WebApp.Services.MealPlanServices;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MealPlanController : ControllerBase
{
    private readonly IMealPlanService _mealPlanService;

    public MealPlanController(IMealPlanService mealPlanService)
    {
        _mealPlanService = mealPlanService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddMealPlan([FromBody] MealPlanDto mealPlanDto)
    {
        var mealPlan = await _mealPlanService.AddMealPlan(mealPlanDto);
        
        return Ok(mealPlanDto);
    }

    [HttpGet("get_all")]
    public async Task<IActionResult> GetMealPlans()
    {
        var mealPlans = await _mealPlanService.GetMealPlans();
        return Ok(mealPlans);
    }

    [HttpGet("get_by_id/{id}")]
    public async Task<IActionResult> GetMealPlanById(int id)
    {
        var mealPlan = await _mealPlanService.GetMealPlanById(id);
        return Ok(mealPlan);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteMealPlanById(int id)
    {
        var mealPlan = await _mealPlanService.DeleteMealPlanById(id);
        return Ok(mealPlan);
    }
}
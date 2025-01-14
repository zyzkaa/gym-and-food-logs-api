using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.DTO.Meals;
using WebApp.Entities;
using WebApp.Services.MealServices;
using WebApp.Utill.Meals;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MealController : ControllerBase
{
    private readonly IMealService _mealService;

    public MealController(IMealService mealService)
    {
        _mealService = mealService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddMeal([FromBody] MealDto mealDto)
    {
        var meal = await _mealService.AddMeal(mealDto);
        var mealDtoResponse = MealUtillity.ParseToMealDto(meal);
        return Ok(mealDtoResponse);
    }

    [HttpGet("get_all")]
    public async Task<IActionResult> GetMeals()
    {
        var meals = await _mealService.GetMeals();
        var mealsDtoResponse = meals.Select(m => MealUtillity.ParseToMealDto(m)).ToList();
        return Ok(mealsDtoResponse);
    }

    [HttpGet("get_by_id/{id}")]
    public async Task<IActionResult> GetMealById(int id)
    {
        var meal = await _mealService.GetMealById(id);
        var mealDtoResponse = MealUtillity.ParseToMealDto(meal);
        return Ok(mealDtoResponse);
    }

    [HttpGet("get_by_product/{id}")]
    public async Task<IActionResult> GetMealsByProduct(int id)
    {
        var meals = await _mealService.GetMealByProductId(id);
        var mealsDtoResposne = meals.Select(m => MealUtillity.ParseToMealDto(m));
        return Ok(mealsDtoResposne);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteMealById(int id)
    {
        var meal = await _mealService.DeleteMealById(id);
        var mealDtoResponse = MealUtillity.ParseToMealDto(meal);
        return Ok(meal);
    }


}
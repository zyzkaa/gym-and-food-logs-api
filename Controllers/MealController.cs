using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.DTO.Meals;
using WebApp.Entities;
using WebApp.Services.MealServices;
using WebApp.Utill.Meals;

namespace WebApp.Controllers;

[ApiController]
[Route("meal")]
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
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            return Unauthorized("Nie można odczytać ID użytkownika.");
        var meal = await _mealService.AddMeal(mealDto,userId);
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
    [HttpGet("get_recent")]
    public async Task<IActionResult> GetMostRecentMeals([FromQuery] int count = 10)
    {
        var meals = await _mealService.GetMostRecentMeals(count);
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

    [HttpGet("getMy")]
    public async Task<IActionResult> GetMyMeals(){
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            return Unauthorized("Nie można odczytać ID użytkownika.");

        var meals = await _mealService.GetMyMeals(userId);
        var mealsDtoResposne = meals.Select(m => MealUtillity.ParseToMealDto(m));
        return Ok(mealsDtoResposne);

    }

    [HttpGet("getMyEdited")]
    public async Task<IActionResult> GetMyEditedMeals(){
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            return Unauthorized("Nie można odczytać ID użytkownika.");

        var meals = await _mealService.GetMyEditedMeals(userId);
        var mealsDtoResposne = meals.Select(m => MealUtillity.ParseToMealDto(m));
        return Ok(mealsDtoResposne);
    }
    
    [HttpGet("get_by_product/{id}")]
    public async Task<IActionResult> GetMealsByProduct(int id)
    {
        var meals = await _mealService.GetMealByProductId(id);
        var mealsDtoResposne = meals.Select(m => MealUtillity.ParseToMealDto(m));
        return Ok(mealsDtoResposne);
    }
    
    [HttpPost("update_calories_and_nutrients")]
    public async Task<IActionResult> UpdateCaloriesAndNutrients([FromBody] List<IngredientInputDto> ingredients)
    {
        if (ingredients == null || !ingredients.Any())
            return BadRequest("Ingredient list is empty.");

        var result = await _mealService.CalculateTotalsAsync(ingredients);

        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteMealById(int id)
    {
        var meal = await _mealService.DeleteMealById(id);
        var mealDtoResponse = MealUtillity.ParseToMealDto(meal);
        return Ok(meal);
    }


}
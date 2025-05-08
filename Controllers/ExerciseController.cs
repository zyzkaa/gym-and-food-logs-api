using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Entities;
using WebApp.Services.ExerciseServices;
using WebApp.Services.TrainingServices;

namespace WebApp.Controllers;

[ApiController]
// [Authorize]
[Route("exercise/")]
public class ExerciseController : ControllerBase
{
    private readonly IExerciseService _exerciseService;

    public ExerciseController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }     
    
    [HttpGet("strength/{id}")]
    public async Task<StrengthExercise> GetStrengthExerciseById(int id)
    {
        return await _exerciseService.GetStrengthExerciseById(id);
    }

    [HttpGet("cardio/{id}")]
    public async Task<CardioExercise> GetCardioExerciseById(int id)
    {
        return await _exerciseService.GetCardioExerciseById(id);
    }

    [HttpGet("cardio")]
    public async Task<List<CardioExercise>> GetCardioExercises()
    {
        return await _exerciseService.GetCardioExercises();
    }
    
    [HttpGet("strength")]
    public async Task<List<StrengthExercise>> GetStrengthExercises()
    {
        return await _exerciseService.GetStrengthExercises();
    }

    [HttpGet("muscle/{id}")]
    public async Task<List<StrengthExercise>> GetExercisesByMuscleId(int id)
    {
        return await _exerciseService.GetExercisesByMuscleId(id);
    }
}
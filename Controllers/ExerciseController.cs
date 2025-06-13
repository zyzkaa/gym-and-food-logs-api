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

    [HttpGet("cardio/search/{name}")]
    public async Task<IEnumerable<Exercise>> SearchCardio(string name)
    {
        return await _exerciseService.GetCardioExercisesBySearch(name);
    }
    
    [HttpGet("strength/search/{name}")]
    public async Task<IEnumerable<Exercise>> SearchStr(string name)
    {
        return await _exerciseService.GetStrExercisesBySearch(name);
    }
    
    [HttpGet("strength/search/{name}/muscle/{id}")]
    public async Task<IEnumerable<Exercise>> SearchStrByMuscleAndSearch(string name, int id)
    {
        return await _exerciseService.GetStrExercisesBySearchAndMuscle(name, id);
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
    
    [HttpGet("strength/latest")]
    public async Task<List<StrengthExercise>> GetRecentStrengthExercises()
    {
        return await _exerciseService.GetRecentStrExercises();
    }

    [HttpGet("muscle/{id}")]
    public async Task<List<StrengthExercise>> GetExercisesByMuscleId(int id)
    {
        return await _exerciseService.GetExercisesByMuscleId(id);
    }

    [HttpGet("muscles")]
    public async Task<List<Muscle>> GetAllMuscles()
    {
        return await _exerciseService.GetAllMuscles();
    }
}
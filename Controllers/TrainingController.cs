using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Entities;
using WebApp.Services.TrainingServices;

namespace WebApp.Controllers;

[ApiController]
[Authorize]
[Route("training")]
public class TrainingController : ControllerBase    
{
    private readonly ITrainingService _trainingService;

    public TrainingController(ITrainingService trainingService)
    {
        _trainingService = trainingService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddTraining(TrainingDto trainingDto)
    {
        var trainingResponse = await _trainingService.AddTraining(trainingDto);
        return Ok(trainingResponse);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteTrainingById(int id)
    {
        var trainingResponse = await _trainingService.DeleteTrainingById(id);
        return Ok(trainingResponse);
    }

    [HttpGet("get_all")]
    public async Task<List<Training>> GetAllTrainings()
    {
        return await _trainingService.GetTrainings();
    }
    
    [HttpGet("get_by_id/{id}")] //????????????
    public async Task<Training> GetTrainingById(int id)
    {
        return await _trainingService.GetTrainingById(id);
    }

    [HttpGet("get_by_date/{date}")]
    public async Task<List<Training>> GetTrainingsByDate(DateOnly date)
    {
        return await _trainingService.GetTrainingsByDate(date);
    }

    [HttpGet("get_strength_exercise/{id}")]
    public async Task<StrengthExercise> GetStrExById(int id)
    {
       return await _trainingService.GetStrengthExerciseById(id);
    }

    [HttpGet("get_cardio_exercise/{id}")]
    public async Task<CardioExercise> GetCarExById(int id)
    {
        return await _trainingService.GetCardioExerciseById(id);
    }

    [HttpGet("get_cardio_exercises")]
    public async Task<List<CardioExercise>> GetCardioExercises()
    {
        return await _trainingService.GetCardioExercises();
    }
    
    [HttpGet("get_strength_exercises")]
    public async Task<List<StrengthExercise>> GetStrengthExercises()
    {
        return await _trainingService.GetStrengthExercises();
    }

    [HttpGet("get_exercises_by_muscle_id/{id}")]
    public async Task<List<StrengthExercise>> GetExercisesByMuscleId(int id)
    {
        return await _trainingService.GetExercisesByMuscleId(id);
    }

    [HttpGet("get_training_by_strength_exercise_id/{id}")]
    public async Task<List<Training>> GetTrainingsByStrengthExerciseId(int id)
    {
        return await _trainingService.GetTrainingsByStrExerciseId(id);
    }
}
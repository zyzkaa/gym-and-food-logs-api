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
    public async Task<IActionResult> GetAllTrainings()
    {
        return Ok(await _trainingService.GetTrainings());
    }
    
    [HttpGet("get_by_id/{id}")] //????????????
    public async Task<IActionResult> GetTrainingById(int id)
    {
        return Ok(await _trainingService.GetTrainingById(id));
    }

    [HttpGet("get_by_date/{date}")]
    public async Task<IActionResult> GetTrainingsByDate(DateOnly date)
    {
        return Ok(await _trainingService.GetTrainingsByDate(date));
    }

    [HttpGet("get_by_strength_exercise_id/{id}")]
    public async Task<IActionResult> GetTrainingsByStrengthExerciseId(int id)
    {
        return Ok(await _trainingService.GetTrainingsByStrengthExerciseId(id));
    }
    
    [HttpGet("get_by_cardio_exercise_id/{id}")]
    public async Task<IActionResult> GetTrainingsByCarExerciseId(int id)
    {
        return Ok(await _trainingService.GetTrainingsByCardioExerciseId(id));
    }

    [HttpGet("get_with_details/{id}")]
    public async Task<IActionResult> GetTrainingsWithDetails(int id)
    {
        return Ok(await _trainingService.GetTrainingWithDetails(id));
    }

    [HttpGet("get_with_records_by_strength_exercise_id/{id}")]
    public async Task<IActionResult> GetTrainingsWithRecordsByStrExerciseId(int id)
    {
        return Ok(await _trainingService.GetTrainigsWithRecordsByStrengthExerciseId(id));
    }
}
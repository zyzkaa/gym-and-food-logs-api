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

    [HttpGet("get_training_by_strength_exercise_id/{id}")]
    public async Task<List<Training>> GetTrainingsByStrExerciseId(int id)
    {
        return await _trainingService.GetTrainingsByStrExerciseId(id);
    }
    
    [HttpGet("get_training_by_cardio_exercise_id/{id}")]
    public async Task<List<Training>> GetTrainingsByCarExerciseId(int id)
    {
        return await _trainingService.GetTrainingsByCarExerciseId(id);
    }

    [HttpGet("get_training_with_details/{id}")]
    public async Task<TrainingResponseDto> GetTrainingsWithDetails(int id)
    {
        return await _trainingService.GetTrainingWithDetails(id);
    }

    [HttpGet("get_trainings_with_records_by_strength_exercise_id/{id}")]
    public async Task<List<Training>> GetTrainingsWithRecordsByStrExerciseId(int id)
    {
        return await _trainingService.GetTrainigsWithRecordsByStrengthExerciseId(id);
    }
}
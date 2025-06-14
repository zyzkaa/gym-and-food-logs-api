using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Entities;
using WebApp.Mappers;
using WebApp.Services.TrainingServices;

namespace WebApp.Controllers;

[ApiController]
// [Authorize]
[Route("training")]
public class TrainingController : ControllerBase    
{
    private readonly ITrainingService _trainingService;

    public TrainingController(ITrainingService trainingService)
    {
        _trainingService = trainingService;
    }

    [HttpPost("")]
    public async Task<IActionResult> AddTraining([FromBody] TrainingRequestDto trainingRequestDto)
    {
        // Console.WriteLine("RECEIVED JSON:");
        // Console.WriteLine(JsonSerializer.Serialize(trainingRequestDto, new JsonSerializerOptions { WriteIndented = true }));

        var trainingResponse = await _trainingService.AddTraining(trainingRequestDto);
        return Ok(trainingResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrainingById(int id)
    {
        var trainingResponse = await _trainingService.DeleteTrainingById(id);
        return Ok(trainingResponse);
    }

    [HttpGet("{page}/{amount}")]
    public async Task<IActionResult> GetAllTrainings(int page, int amount)
    {
        var trainings = await _trainingService.GetTrainings(page, amount);
        return Ok(trainings.Select(TrainingMapper.ToShortResponseDto).ToList());
    }
    
    // [HttpGet("id/{id}")] //????????????
    // public async Task<IActionResult> GetTrainingById(int id)
    // {
    //     return Ok(await _trainingService.GetTrainingById(id));
    // }

    [HttpGet("date/{date}")]
    public async Task<IActionResult> GetTrainingsByDate(DateOnly date)
    {
        return Ok(await _trainingService.GetTrainingsByDate(date));
    }

    [HttpGet("strength_id/{id}")]
    public async Task<IActionResult> GetTrainingsByStrengthExerciseId(int id)
    {
        return Ok(await _trainingService.GetTrainingsByStrengthExerciseId(id));
    }
    
    [HttpGet("cardio_id/{id}")]
    public async Task<IActionResult> GetTrainingsByCarExerciseId(int id)
    {
        return Ok(await _trainingService.GetTrainingsByCardioExerciseId(id));
    }

    [HttpGet("{id}")] //????????????
    public async Task<IActionResult> GetTrainingsWithDetails(int id)
    {
        return Ok(await _trainingService.GetTrainingWithDetails(id));
    }

    [HttpGet("details/strength_id/{id}")]
    public async Task<IActionResult> GetTrainingsWithRecordsByStrExerciseId(int id)
    {
        return Ok(await _trainingService.GetTrainigsWithRecordsByStrengthExerciseId(id));
    }
}
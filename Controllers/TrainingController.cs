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
    public async Task<Training> SetCurrentTraining(int id)
    {
        return await _trainingService.DeleteTrainingById(id);
    }
    

    [HttpGet("get_all")]
    public async Task<IEnumerable<Training>> GetAllTrainings()
    {
        return await _trainingService.GetTrainings();
    }
    
    [HttpGet("get/{id}")] //????????????
    public async Task<Training> GetTrainingById(int id)
    {
        return await _trainingService.GetTrainingById(id);
    }

    [HttpGet("get_strength_exercise/{id}")]
    public async Task<StrengthExercise> Get(int id)
    {
       return await _trainingService.GetStrengthExerciseById(id);
    }
}
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
    public async Task<IActionResult> AddTraining([FromBody] TrainingDto trainingDto)
    {
        var trainingResponseDto = await _trainingService.AddTraining(trainingDto);
        return Ok(trainingResponseDto);
    }

    [HttpPost("set_current")]
    public async Task<TrainingResponseDto> SetCurrentTraining(int id)
    {
        return await _trainingService.SetCurrentTraining(id);
    }

    [HttpGet("get_current")]
    public Training GetCurrentTraining()
    {
        return _trainingService.GetCurrentTraining();
    }
}
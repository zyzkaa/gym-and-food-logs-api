using Microsoft.AspNetCore.Mvc;
using WebApp.Services.ImagesServices;

namespace WebApp.Controllers;

[ApiController]
[Route("image")]
public class ImageController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly IImagesService _imagesService;

    public ImageController(IWebHostEnvironment env, IImagesService imagesService)
    {
        _env = env;
        _imagesService = imagesService;
    }
    
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Invalid file.");

        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var url = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
        return Ok(new { url });
    }
    [HttpGet("get_by_mealId/{mealId}")]
    public async Task<IActionResult> GetImageByMealId(int mealId)
    {
        var result = await _imagesService.GetImageByMealIdAsync(mealId, Request);
        if (result == null)
            return NotFound("Meal or image not found.");

        return result;
    }
}

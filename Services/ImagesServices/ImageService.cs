using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Services.ImagesServices;

namespace WebApp.Services.ImagesServices;

public class ImagesService : IImagesService
{
    private readonly IWebHostEnvironment _env;
    private readonly WebAppContext _dbContext;
    private readonly ILogger<ImagesService> _logger;

    public ImagesService(IWebHostEnvironment env, WebAppContext dbContext, ILogger<ImagesService> logger)
    {
        _env = env;
        _dbContext = dbContext;
        _logger = logger;
    }


    public async Task<FileContentResult?> GetImageByMealIdAsync(int mealId, HttpRequest request)
    {
        var meal = await _dbContext.Meals
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == mealId);

        if (meal == null || string.IsNullOrWhiteSpace(meal.ImageURL))
            return null;

        var imagePath = Path.Combine(_env.WebRootPath, meal.ImageURL.Replace("/", Path.DirectorySeparatorChar.ToString()));
        _logger.LogInformation("Resolved image path: {ImagePath}", imagePath);


        if (!File.Exists(imagePath))
            return null;

        var imageBytes = await File.ReadAllBytesAsync(imagePath);
        const string contentType = "image/png"; // Adjust to detect content if needed

        return new FileContentResult(imageBytes, contentType);
    }
}
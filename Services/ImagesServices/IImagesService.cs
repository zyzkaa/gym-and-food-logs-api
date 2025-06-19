using Microsoft.AspNetCore.Mvc;

namespace WebApp.Services.ImagesServices;

public interface IImagesService
{
    Task<FileContentResult?> GetImageByMealIdAsync(int mealId, HttpRequest request);
}
using Microsoft.EntityFrameworkCore;
using WebApp.Entities;

namespace WebApp.Services.TrainingServices;

public static class TrainingExtensions
{
    public static IQueryable<Training> GetTrainingSummary(this IQueryable<Training> query)
    {
        return query
            .Include(t => t.StrExercises)
            .ThenInclude(e => e.StrengthExercise)
            .Include(t => t.CarExercises)
            .ThenInclude(e => e.CardioExercise);
    }
}
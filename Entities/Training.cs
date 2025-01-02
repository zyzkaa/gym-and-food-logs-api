using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApp.Entities;

public class Training
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan? Duration { get; set; } = TimeSpan.Zero;
    public ICollection<StrExerciseInTraining> StrExercises { get; set; } = new List<StrExerciseInTraining>();
    public ICollection<CarExerciseInTraining> CarExercises { get; set; } = new List<CarExerciseInTraining>();
    [JsonIgnore]
    public User User { get; set; }
}   
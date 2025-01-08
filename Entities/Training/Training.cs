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
    public ICollection<StrengthExerciseInTraining> StrengthExercises { get; set; } = new List<StrengthExerciseInTraining>();
    public ICollection<CardioExerciseInTraining> CardioExercises { get; set; } = new List<CardioExerciseInTraining>();
    [JsonIgnore]
    public User User { get; set; }
}   
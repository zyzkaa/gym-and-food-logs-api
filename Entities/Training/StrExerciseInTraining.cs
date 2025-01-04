using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApp.Entities;

public class StrExerciseInTraining
{
    public int Id { get; set; }
    public StrengthExercise StrengthExercise { get; set; }
    public ICollection<StrParams> StrParams { get; set; } = new List<StrParams>();
    [JsonIgnore]
    public int TrainingId { get; set; }  // Klucz obcy do Training
    [JsonIgnore]
    public Training Training { get; set; }
}
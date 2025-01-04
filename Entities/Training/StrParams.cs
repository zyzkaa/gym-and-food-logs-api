using System.Text.Json.Serialization;

namespace WebApp.Entities;

public class StrParams
{
    public int Id { get; set; }
    public int Set { get; set; }
    public int Weight { get; set; }
    public int Repetitions { get; set; }
    [JsonIgnore]
    public StrExerciseInTraining StrExerciseInTraining { get; set; }
    [JsonIgnore]
    public int StrExerciseInTrainingId { get; set; }
}
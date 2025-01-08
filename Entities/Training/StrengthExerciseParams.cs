using System.Text.Json.Serialization;

namespace WebApp.Entities;

public class StrengthExerciseParams
{
    public int Id { get; set; }
    public int Set { get; set; }
    public int Weight { get; set; }
    public int Repetitions { get; set; }
    [JsonIgnore]
    public StrengthExerciseInTraining StrengthExerciseInTraining { get; set; }
}
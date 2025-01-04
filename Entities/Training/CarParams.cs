using System.Text.Json.Serialization;

namespace WebApp.Entities;

public class CarParams
{
    public int Id { get; set; }
    public int Inteval { get; set; }
    public int Speed { get; set; }
    public TimeSpan Time { get; set; }
    [JsonIgnore]
    public CarExerciseInTraining CarExerciseInTraining { get; set; }
    [JsonIgnore]
    public int CarExerciseInTrainingId { get; set; }
}
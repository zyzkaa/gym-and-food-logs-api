using System.Text.Json.Serialization;

namespace WebApp.Entities;

public class CarParams
{
    public int Id { get; set; }
    public int Inteval { get; set; }
    public double Speed { get; set; }
    public TimeSpan Time { get; set; }
    [JsonIgnore]
    public CarExerciseInTraining CarExerciseInTraining { get; set; }
}
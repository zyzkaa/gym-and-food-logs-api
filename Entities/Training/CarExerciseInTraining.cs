using System.Text.Json.Serialization;

namespace WebApp.Entities;

public class CarExerciseInTraining
{
    public int Id { get; set; }
    public CardioExercise CardioExercise { get; set; }
    public ICollection<CarParams> CarParams { get; set; } = new List<CarParams>();
    [JsonIgnore]
    public int TrainingId { get; set; }  // Klucz obcy do Training
    [JsonIgnore]
    public Training Training { get; set; }
}
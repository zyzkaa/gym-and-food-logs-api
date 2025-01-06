using System.Text.Json.Serialization;

namespace WebApp.Entities;

public class CardioExerciseInTraining
{
    public int Id { get; set; }
    public CardioExercise CardioExercise { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<CardioExerciseParams> Params { get; set; }
    [JsonIgnore]
    public Training Training { get; set; }
}
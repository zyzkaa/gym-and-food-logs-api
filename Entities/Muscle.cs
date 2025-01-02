using System.Text.Json.Serialization;

namespace WebApp.Entities;

public class Muscle
{
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public ICollection<StrengthExercise> StrengthExercises { get; set; }
}
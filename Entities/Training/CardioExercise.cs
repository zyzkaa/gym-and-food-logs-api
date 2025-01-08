using System.Text.Json.Serialization;

namespace WebApp.Entities;

public class CardioExercise : Exercise
{
    [JsonIgnore]
    public ICollection<Met> Mets { get; set; }
}
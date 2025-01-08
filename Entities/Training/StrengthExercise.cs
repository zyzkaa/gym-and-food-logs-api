using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApp.Entities;

public class StrengthExercise : Exercise
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<Muscle>? Muscles { get; set; }
}
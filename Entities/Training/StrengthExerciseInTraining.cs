using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApp.Entities;

public class StrengthExerciseInTraining
{
    public int Id { get; set; }
    public StrengthExercise StrengthExercise { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<StrengthExerciseParams> Params { get; set; }
    [JsonIgnore]
    public Training Training { get; set; }
}
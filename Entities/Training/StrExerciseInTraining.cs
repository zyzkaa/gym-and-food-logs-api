using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApp.Entities;

public class StrExerciseInTraining
{
    public int Id { get; set; }
    public StrengthExercise StrengthExercise { get; set; }
    public ICollection<StrParams> StrParams { get; set; } = new List<StrParams>();
}
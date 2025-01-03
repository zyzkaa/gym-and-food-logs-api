using Swashbuckle.AspNetCore.Annotations;

namespace WebApp.Entities;

public class StrengthExercise : Exercise
{
    public ICollection<Muscle> Muscles { get; set; }
}
namespace WebApp.Entities;

public class CardioExercise : Exercise
{
    public ICollection<Met> Mets { get; set; }
}
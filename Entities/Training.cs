namespace WebApp.Entities;

public class Training
{
    public int Id { get; set; }
    public string Name { get; set; }
    public User? User { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan? Duration { get; set; }
    public ICollection<StrExerciseInTraining> StrExerciseInTrainings{ get; set; }
    public ICollection<CarExerciseInTraining> CarExerciseInTrainings{ get; set; }

    public Training(string name, DateTime date)
    {
        Name = name;
        Date = date;
    }
}   
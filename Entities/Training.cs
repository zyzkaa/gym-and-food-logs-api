using System.ComponentModel.DataAnnotations;

namespace WebApp.Entities;

public class Training
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    public User? User { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan? Duration { get; set; }
    public ICollection<StrExerciseInTraining> StrExerciseInTrainings { get; set; } = new List<StrExerciseInTraining>();
    public ICollection<CarExerciseInTraining> CarExerciseInTrainings{ get; set; } = new List<CarExerciseInTraining>();

    public Training(){}
    public Training(string name, DateOnly date, TimeSpan duration, User user, ICollection<StrExerciseInTraining> strExerciseInTrainings, ICollection<CarExerciseInTraining> carExerciseInTrainings)
    {
        Name = name;
        Date = date;
        User = user;
        Duration = duration;
        StrExerciseInTrainings = strExerciseInTrainings;
        CarExerciseInTrainings = carExerciseInTrainings;
    }
    public Training(string name, DateOnly date, TimeSpan duration, User user)
    {
        Name = name;
        Date = date;
        User = user;
        Duration = duration;
    }
}   
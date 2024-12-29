using System.ComponentModel.DataAnnotations;

namespace WebApp.Entities;

public class Training
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    public User? User { get; set; }
    public DateTime StartDate { get; set; }
    public TimeSpan? Duration { get; set; }
    public ICollection<StrExerciseInTraining> StrExerciseInTrainings{ get; set; }
    public ICollection<CarExerciseInTraining> CarExerciseInTrainings{ get; set; }

    public Training(){}
    public Training(string name, DateTime startDate, TimeSpan duration, User user)
    {
        Name = name;
        StartDate = startDate;
        User = user;
        Duration = duration;
        StrExerciseInTrainings = new List<StrExerciseInTraining>();
        CarExerciseInTrainings = new List<CarExerciseInTraining>();
    }
}   
using WebApp.Entities;

namespace WebApp.Services.TrainingServices;

public interface ICurrentTrainingSerivce
{
    public void SetCurrentTraining (Training training);
    public Training GetCurrentTraining ();
}
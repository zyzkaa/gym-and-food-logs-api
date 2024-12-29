using WebApp.Entities;

namespace WebApp.Services.TrainingServices;

public class CurrentTrainingService : ICurrentTrainingSerivce
{
    private Training? currentTraining = null;
    
    public void SetCurrentTraining(Training training)
    {
        currentTraining = training;
    }

    public Training GetCurrentTraining()
    {
        return currentTraining;
    }
}
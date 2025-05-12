using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Mappers;

public static class TrainingMapper
{
    public static TrainingShortResponseDto ToShortResponseDto(this Training training)
    {
        var exerciseList = new List<String>();
        foreach (var exercise in training.CardioExercises)
        {
            exerciseList.Add(exercise.CardioExercise.Name);
        }
        foreach (var exercise in training.StrengthExercises)
        {
            exerciseList.Add(exercise.StrengthExercise.Name);
        }

        return new TrainingShortResponseDto(
            training.Id,
            training.Name,
            training.Date,
            training.Duration ?? TimeSpan.Zero,
            exerciseList);
    }
    
    public static TrainingResponseDtoRec ToResponseDto(this Training training, User user)
    {
        var strExercises = new List<TrainingResponseDto.StrengthExerciseResponseDto>();
    
        foreach (var strExerciseInTraining in training.StrengthExercises)
        {
            var paramsList = new List<TrainingResponseDto.StrParamsResponseDto>();
            double volume = 0;
    
            foreach (var strParams in strExerciseInTraining.Params)
            {
                var newParams = new TrainingResponseDto.StrParamsResponseDto(strParams.Set, strParams.Weight,
                    strParams.Repetitions, strParams.Weight * strParams.Repetitions);
    
                paramsList.Add(newParams);
                volume += newParams.Volume;
            }
    
            var newExercise = new TrainingResponseDto.StrengthExerciseResponseDto(
                strExerciseInTraining.StrengthExercise, paramsList, volume);
    
            strExercises.Add(newExercise);
        }
        
        double totalCalories = 0;
        var carExercises = new List<TrainingResponseDto.CarExerciseResponseDto>();
        foreach (var carExerciseInTraining in training.CardioExercises)
        {
            var mets = carExerciseInTraining.CardioExercise.Mets
                .Where(met => met.cardioExercise.Id == carExerciseInTraining.CardioExercise.Id)
                .OrderByDescending(met => met.MetValue);
            
            var paramsList = new List<TrainingResponseDto.CarParamsResponseDto>();
            double caloriesInExercise = 0;
            TimeSpan totalTime = TimeSpan.Zero;
    
            foreach (var carParams in carExerciseInTraining.Params)
            {
                var metValue = mets
                    .FirstOrDefault(met => met.StartSpeed <= carParams.Speed)
                    .MetValue;
                var timeInHours = carParams.Time.TotalHours;
                var calories = metValue * timeInHours * user.Weight;
    
                var newParams = new TrainingResponseDto.CarParamsResponseDto(carParams.Inteval,
                    carParams.Speed, carParams.Time, calories);
    
                paramsList.Add(newParams);
                caloriesInExercise += calories;
                totalTime += carParams.Time;
            }
            
            var newExercise = new TrainingResponseDto.CarExerciseResponseDto(carExerciseInTraining.CardioExercise,
                paramsList, caloriesInExercise, totalTime);
            totalCalories += caloriesInExercise;
            carExercises.Add(newExercise);
        }
    
        return new TrainingResponseDtoRec(
            training.Id,
            training.Name,
            training.Duration ?? TimeSpan.Zero,
            training.Date,
            strExercises,
            carExercises,
            totalCalories);
    
    }

    public static Training FromDto(this TrainingRequestDto dto, User user, List<CardioExercise> carExercises, List<StrengthExercise> strExercises)
    {
        Training newTraining = new Training()
        {
            Name = dto.Name,
            Date = dto.Date,
            Duration = dto.Duration,
            User = user
        };
        
        foreach (var strExerciseInTraining in dto.StrengthExercises)
        {
            StrengthExerciseInTraining newStrEx = new StrengthExerciseInTraining()
            {
                StrengthExercise = strExercises.Find(s => s.Id == strExerciseInTraining.ExerciseId) ?? throw new InvalidOperationException(),
                Params = new List<StrengthExerciseParams>()
            };
            
            foreach (var strParams in strExerciseInTraining.Params)
            { 
                var newParams = new StrengthExerciseParams()
                {
                    Set = strParams.Set,
                    Weight = strParams.Weight,
                    Repetitions = strParams.Repetitions
                };
                newStrEx.Params.Add(newParams);
            }
            
            newTraining.StrengthExercises.Add(newStrEx);
        }
        
        foreach (var carExerciseInTraining in dto.CardioExercises)
        {
            CardioExerciseInTraining newCarEx = new CardioExerciseInTraining()
            {
                CardioExercise = carExercises.Find(c => c.Id == carExerciseInTraining.ExerciseId) ?? throw new InvalidOperationException(),
                Params = new List<CardioExerciseParams>()
            };

            foreach (var carParams in carExerciseInTraining.Params)
            {
                var newParams = new CardioExerciseParams()
                {
                    Inteval = carParams.Interval,
                    Speed = carParams.Speed,
                    Time = carParams.Time
                };
                newCarEx.Params.Add(newParams);
            }
            
            newTraining.CardioExercises.Add(newCarEx);
        }

        return newTraining;
    }
}
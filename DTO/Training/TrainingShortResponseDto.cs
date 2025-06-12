namespace WebApp.DTO;

public record TrainingShortResponseDto(
    int id, 
    string name, 
    DateOnly date, 
    TimeSpan duration, 
    List<String> exercises);
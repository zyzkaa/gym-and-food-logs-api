namespace WebApp.DTO;

public record RemindersDto(
    TimeOnly Time,
    List<int> Days
    );
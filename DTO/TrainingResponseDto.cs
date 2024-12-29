namespace WebApp.DTO;

public class TrainingResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public TrainingResponseDto(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
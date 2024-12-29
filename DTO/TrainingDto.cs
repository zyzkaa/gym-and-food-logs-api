using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO;

public class TrainingDto
{
    [MaxLength(50)]
    public string Name { get; set; }
    public int Hours { get; set; }
    [Range(1, 59)]
    public int Minutes { get; set; }
    [Range(1,24)]
    public int StartHour { get; set; }
    [Range(1,59)]
    public int StartMinute { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO;

public class DateDto
{
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}
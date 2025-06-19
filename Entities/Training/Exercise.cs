using Swashbuckle.AspNetCore.Annotations;

namespace WebApp.Entities;

public class Exercise
{
    public int Id { get; set; }
    public string NamePl { get; set; }
    public string NameEn { get; set; }
}
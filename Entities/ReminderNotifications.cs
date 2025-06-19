using System.Collections;

namespace WebApp.Entities;

public class  ReminderNotifications
{
    public int Id { get; set; }
    public TimeOnly Time { get; set; }
    public List<WeekDays> Days { get; set; } = new List<WeekDays>();
}
namespace WebApp.Services.NotificationServices;

public class WeeklyNotificationService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextSunday = now.AddDays((7 - (int)now.DayOfWeek) % 7);
            var nextRunTime = new DateTime(nextSunday.Year, nextSunday.Month, nextSunday.Day, 20, 0, 0);

            var delay = nextRunTime - now;
            if (delay < TimeSpan.Zero) delay += TimeSpan.FromDays(7); 

            Console.WriteLine($"[WeeklyNotificationService] Sleeping until {nextRunTime}");

            await Task.Delay(delay, stoppingToken);

            try
            {
                await TrainingNotificationService.SendAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WeeklyNotificationService] Błąd: {ex.Message}");
            }

            await Task.Delay(TimeSpan.FromDays(7), stoppingToken);
        }
    }
}
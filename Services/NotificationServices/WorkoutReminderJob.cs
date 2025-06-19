namespace WebApp.Services.NotificationServices;

using Quartz;
using WebApp.Services.NotificationServices;

[DisallowConcurrentExecution]
public class WorkoutReminderJob : IJob
{

    public async Task Execute(IJobExecutionContext context)
    {
        var token = context.MergedJobDataMap.GetString("token");
        var userId = context.MergedJobDataMap.GetInt("userId");

        Console.WriteLine($"[QUARTZ] Reminder job started for user {userId}");

        if (!string.IsNullOrWhiteSpace(token))
        {
            await TrainingNotificationService.SendPersonal(token);
        }
    }
}

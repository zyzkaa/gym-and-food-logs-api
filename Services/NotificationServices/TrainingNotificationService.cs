using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;

namespace WebApp.Services.NotificationServices;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


public static class TrainingNotificationService
{
    private static HttpClient client;

    static TrainingNotificationService()
    {
        client = new HttpClient();
        // client.BaseAddress = new Uri("https://fcm.googleapis.com/v1/projects");
        // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public static async Task SendAsync()
    {
        var serviceAccountPath = "Services/NotificationServices/firebase.json"; 
        var scopes = new[] { "https://www.googleapis.com/auth/firebase.messaging" };

        var credential = GoogleCredential.FromFile(serviceAccountPath).CreateScoped(scopes);
        var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        
        var json = await File.ReadAllTextAsync(serviceAccountPath);
        var firebaseConfig = JsonSerializer.Deserialize<FirebaseServiceAccount>(json);
        var projectId = firebaseConfig?.ProjectId ?? throw new Exception("no project_id in firebase.json");

        var url = $"https://fcm.googleapis.com/v1/projects/{projectId}/messages:send";

        var message = new
        {
            message = new
            {
                topic = "all",
                notification = new
                {
                    title = "weekly_reminder_title",
                    body = "weekly_reminder_body",
                },
                data = new
                {
                    force = "true"
                }
            }
        };

        var jsonMessage = JsonConvert.SerializeObject(message);

        using var requestClient = new HttpClient();
        requestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        requestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
        var response = await requestClient.PostAsync(url, content);

        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Status: {response.StatusCode}");
        Console.WriteLine($"Odpowiedź: {responseBody}");
    }
    

}
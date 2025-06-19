namespace WebApp.Services.NotificationServices;

using System.Text.Json;
using System.Text.Json.Serialization;

public class FirebaseServiceAccount
{
    [JsonPropertyName("project_id")]
    public string ProjectId { get; set; }
}

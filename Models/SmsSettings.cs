namespace NotificationManager.Models
{
    public class SmsSettings
    {
        public string Provider { get; set; } = string.Empty;
        public string FromNumber { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
}

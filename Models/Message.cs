namespace NotificationManager.Models
{
    public enum MessageChannel
    {
        Sms,
        Email
    }

    public class Message
    {
        public MessageChannel Channel { get; set; }
        public string To { get; set; } = string.Empty;
        public string? Subject { get; set; }
        public string Body { get; set; } = string.Empty;
    }
}

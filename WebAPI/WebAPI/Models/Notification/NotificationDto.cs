namespace WebAPI.Models.Notification
{
    public class NotificationDto
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime SendTime { get; set; }
        public bool IsRead { get; set; }
    }
}

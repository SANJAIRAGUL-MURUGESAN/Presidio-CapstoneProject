namespace BloggingApp.Models
{
    public class UserNotification
    {
        public int Id { get; set; }
        public string NotificatioContent { get; set; }
        public string NotificationPost { get; set; }
        public string IsUserSeen { get; set; }
        public int UserId { get; set; }
        public DateTime ContentDateTime {get;set;}
    }
}

namespace BloggingApp.Exceptions.UserNotifications
{
    public class NoSuchUserNotificationFoundException:Exception
    {
        string msg;
        public NoSuchUserNotificationFoundException()
        {
            msg = "No Such User Notification Found!";
        }
        public override string Message => msg;
    }
}

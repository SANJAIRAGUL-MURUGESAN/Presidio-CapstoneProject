namespace BloggingApp.Exceptions.UserNotifications
{
    public class NoUserNotificationsFoundException:Exception
    {
        string msg;
        public NoUserNotificationsFoundException()
        {
            msg = "No Such User Notification Found!";
        }
        public override string Message => msg;
    }
}

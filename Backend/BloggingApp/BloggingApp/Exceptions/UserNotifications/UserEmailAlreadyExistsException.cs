namespace BloggingApp.Exceptions.UserNotifications
{
    public class UserEmailAlreadyExistsException:Exception
    {

        string msg;
        public UserEmailAlreadyExistsException()
        {
            msg = "User Email or UserName Already Exists! Please Try Again with a Different One!";
        }
        public override string Message => msg;
    }
}

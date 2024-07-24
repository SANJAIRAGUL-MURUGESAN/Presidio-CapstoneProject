namespace BloggingApp.Exceptions.UserExceptions
{
    public class NoUsersFoundException :Exception
    {
        string msg;
        public NoUsersFoundException()
        {
            msg = "No Users Found!";
        }
        public override string Message => msg;
    }
}

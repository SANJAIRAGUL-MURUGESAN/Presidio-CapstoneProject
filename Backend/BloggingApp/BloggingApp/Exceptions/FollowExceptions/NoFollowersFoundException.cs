namespace BloggingApp.Exceptions.FollowExceptions
{
    public class NoFollowersFoundException : Exception
    {
        string msg;
        public NoFollowersFoundException()
        {
            msg = "No Followers Found!";
        }
        public override string Message => msg;
    }
}

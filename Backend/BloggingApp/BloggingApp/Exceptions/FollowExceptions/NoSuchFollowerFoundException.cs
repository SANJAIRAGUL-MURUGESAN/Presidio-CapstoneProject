namespace BloggingApp.Exceptions.FollowExceptions
{
    public class NoSuchFollowerFoundException :Exception
    {
        string msg;
        public NoSuchFollowerFoundException()
        {
            msg = "No Such Follower Found!";
        }
        public override string Message => msg;
    }
}

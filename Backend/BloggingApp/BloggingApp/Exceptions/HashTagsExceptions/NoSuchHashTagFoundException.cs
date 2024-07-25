namespace BloggingApp.Exceptions.HashTagsExceptions
{
    public class NoSuchHashTagFoundException:Exception
    {
        string msg;
        public NoSuchHashTagFoundException()
        {
            msg = "No Such hashTag Found!";
        }
        public override string Message => msg;
    }
}

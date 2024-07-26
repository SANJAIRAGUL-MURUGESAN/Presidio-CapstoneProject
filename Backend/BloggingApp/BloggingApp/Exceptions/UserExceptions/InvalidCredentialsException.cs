namespace BloggingApp.Exceptions.UserExceptions
{
    public class InvalidCredentialsException : Exception
    {
        string msg;
        public InvalidCredentialsException()
        {
            msg = "Invalid Username or Password!";
        }
        public override string Message => msg;
    }
}

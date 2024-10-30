namespace Application.Common.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string msg) : base(msg) { }
    }
}

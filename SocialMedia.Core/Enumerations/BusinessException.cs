namespace SocialMedia.Core.Enumerations
{
    public class BusinessException : Exception
    {
        public BusinessException()
        { }
        public BusinessException(string message) : base(message)
        { }
    }
}

namespace Common.Domain.Exceptions
{
    public class InvalidDomainDataException : BaseDomainExceotion
    {
        public InvalidDomainDataException()
        {

        }

        public InvalidDomainDataException(string message) : base(message)
        {
        }
    }
}

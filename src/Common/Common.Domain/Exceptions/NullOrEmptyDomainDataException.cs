namespace Common.Domain.Exceptions
{
    public class NullOrEmptyDomainDataException:BaseDomainExceotion
    {
        public NullOrEmptyDomainDataException() { }
        public NullOrEmptyDomainDataException(string message):base(message)
        {

        }
        public static void CheckString(params (string Value, string Key)[] items)
        {
            foreach (var (value, Key) in items)
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NullOrEmptyDomainDataException($"{Key} is null or empty");
                }
                  
            }
        }
    }
}

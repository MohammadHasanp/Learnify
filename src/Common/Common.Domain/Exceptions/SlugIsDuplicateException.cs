namespace Common.Domain.Exceptions
{
    public class SlugIsDuplicateException : BaseDomainExceotion
    {
        public SlugIsDuplicateException() : base("Slug Is Null"){}

        public SlugIsDuplicateException(string message) : base(message){}
    }

}

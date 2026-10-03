
using Common.Domain.Exceptions;

namespace Common.Domain.ValueObjects
{
    public class PhoneNumber:ValueObject
    {
        public PhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length is < 11 or > 11)
                throw new InvalidDomainDataException("شماره تلفن نامعتبر است");

            Value = value;
        }

        public string Value { get; private set; }



    }
}

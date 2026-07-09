using System.Net.Mail;
using BikeShop.Domain.Exceptions;

namespace BikeShop.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if(string.IsNullOrWhiteSpace(value)) {
                throw new DomainValidationException("Email address cannot be empty");
            }
            value = value.Trim();

            try {
                Value = new MailAddress(value).Address;
            }
            catch (FormatException ex) {
                throw new DomainValidationException($"Email address {value} has wrong format. {ex.Message}");
            }

        }
    }
}

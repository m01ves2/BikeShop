using System.Numerics;
using BikeShop.Domain.Exceptions;
using BikeShop.Domain.ValueObjects;

namespace BikeShop.Domain.Entities
{
    public class Customer
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
        
        public int ApplicationUserId { get; set; }

        public Cart Cart { get; set; } = null!;
        public int CartId { get; set; }

        public Customer(Email email, string firstname="", string lastname="", string phone = "", string address = "")
        {
            ChangeEmail(email);
            ChangeFirstName(firstname);
            ChangeLastName(lastname);
            ChangePhone(phone);
            ChangeAddress(address);
        }

        public void ChangeEmail(Email email)
        {
            if (email == null)
                throw new DomainValidationException("Email cannot be null");

            Email = email;
        }

        public void ChangeFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName)) {
                FirstName = "";
                return;
            }
            FirstName = firstName.Trim();
        }

        public void ChangeLastName(string lastName) 
        {
            if (string.IsNullOrWhiteSpace(lastName)) {
                LastName = "";
                return;
            }
            LastName = lastName.Trim();
        }

        public void ChangePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) {
                Phone = "";
                return;
            }
            Phone = phone.Trim();
        }
        public void ChangeAddress(string address) 
        {
            if (string.IsNullOrWhiteSpace(address)) {
                Address = "";
                return;
            }
            Address = address.Trim();
        }
    }
}

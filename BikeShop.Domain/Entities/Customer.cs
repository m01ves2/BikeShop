using System.Numerics;
using BikeShop.Domain.Exceptions;
using BikeShop.Domain.ValueObjects;

namespace BikeShop.Domain.Entities
{
    public class Customer
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public string Address { get; private set; } = string.Empty;

        public int ApplicationUserId { get; private set; }

        public Cart Cart { get; set; } = null!;

        public List<Order> Orders { get; private set; }

        private Customer()
        {
        }

        public Customer(int applicationUserId)
        {
            ApplicationUserId = applicationUserId;
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

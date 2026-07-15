using BikeShop.Domain.Exceptions;

namespace BikeShop.Domain.Entities
{
    public class Category
    {
        public const int MaxNameLength = 100;
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Category(string name)
        {
            ChangeName(name);
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) {
                throw new DomainValidationException("Name cannot be empty");
            }

            name = name.Trim();
            if (name.Length > MaxNameLength)
                throw new DomainValidationException($"Name cannot be longer than {MaxNameLength} chars");

            Name = name;
        }
    }
}

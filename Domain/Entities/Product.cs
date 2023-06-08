using System;
using FluentValidation;

namespace ProductsApp.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string? Description { get; private set; }
        public bool IsDeleted { get; private set; }

        public Product(Guid id, string name, decimal price, string description)
        {
            Id = id;
            Name = name;
            SetPrice(price);
            Description = description;
            IsDeleted = false;
        }

        public void Update(string name, decimal price, string description)
        {
            Name = name;
            SetPrice(price);
            Description = description;
        }

        private void SetPrice(decimal price)
        {
            if (price <= 0)
            {
                throw new ValidationException("Price must be greater than 0.");
            }

            Price = price;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}

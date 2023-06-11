using System;
using FluentValidation;

namespace ProductsApp.Application.Commands
{
    public class UpdateProductCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Price).GreaterThan(0);
            // making description as optional
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}
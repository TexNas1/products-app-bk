using FluentValidation;

namespace ProductsApp.Application.Commands
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Price).GreaterThan(0);
            // making description as optional
            RuleFor(x => x.Description).MaximumLength(500);

        }
    }
}

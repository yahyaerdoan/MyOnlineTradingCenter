using FluentValidation;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Products.Commands.Create;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Validations.Products;

public class CreateProductValidator : AbstractValidator<CreateProductCommandRequest>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.CreateProductDto.Name)
           .NotEmpty()
           .WithMessage("The product name cannot be empty or null.")
           .MaximumLength(150)
           .WithMessage("The product name cannot exceed 150 characters.")
           .MinimumLength(2)
           .WithMessage("The product name must be at least 2 characters long.");

        RuleFor(p => p.CreateProductDto.Description)
            .NotEmpty()
            .WithMessage("The product description cannot be empty or null.")
            .MaximumLength(200)
            .WithMessage("The product description cannot exceed 200 characters.")
            .MinimumLength(5)
            .WithMessage("The product description must be at least 5 characters long.");

        RuleFor(p => p.CreateProductDto.Stock)
            .NotNull()
            .WithMessage("The product stock cannot be null.")
            .GreaterThanOrEqualTo(0)
            .WithMessage("The product stock cannot be less than zero.");

        RuleFor(p => p.CreateProductDto.Price)
            .NotNull()
            .WithMessage("The product price cannot be null.")
            .GreaterThanOrEqualTo(0)
            .WithMessage("The product price cannot be less than zero.");
    }
}

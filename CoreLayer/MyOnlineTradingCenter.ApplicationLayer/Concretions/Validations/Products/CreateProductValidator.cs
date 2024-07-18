using FluentValidation;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Validations.Products;

public class CreateProductValidator : AbstractValidator<CreateProductViewModel>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("The product name can not be empty or null")
            .MaximumLength(150)
            .MinimumLength(2)
            .WithMessage("The product name can not be less then two character.");

        RuleFor(p => p.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("The product description can not be empty or null")
            .MaximumLength(200)
            .MinimumLength(5)
            .WithMessage("The product description can not be less then five character.");

        RuleFor(p => p.Stock)
           .NotEmpty()
           .NotNull()
           .WithMessage("The product stock can not be empty or null")
           .Must(s => s >= 0)
           .WithMessage("The product stock can not be less then zero or negative.");

        RuleFor(p => p.Price)
            .GreaterThanOrEqualTo(0)
                .WithMessage("The product price cannot be less than zero or negative.");
        //.NotEmpty()
        //.NotNull()
        //.WithMessage("The product price can not be empty or null")
        //.Must(s => s >= 0)
        //.WithMessage("The product price can not be less then zero or negative.");
    }
}

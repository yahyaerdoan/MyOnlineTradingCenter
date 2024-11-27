using FluentValidation;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.CompletedOrders.Commands.Create;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Validations.CompletedOrders;

public class CompleteOrderValidator : AbstractValidator<CompleteOrderCommandRequest>
{
    public CompleteOrderValidator()
    {
        RuleFor(x => x.CompleteOrderDto.OrderId)
            .NotEmpty().WithMessage("OrderId cannot be empty.") // Equivalent to string.IsNullOrWhiteSpace
            .Must(id => Guid.TryParse(id, out var guid) && guid != Guid.Empty)
            .WithMessage("OrderId must be a valid non-empty GUID."); // Equivalent to Guid.TryParse
    }
}

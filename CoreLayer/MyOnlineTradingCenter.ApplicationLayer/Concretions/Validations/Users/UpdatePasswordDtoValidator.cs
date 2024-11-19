using FluentValidation;
using MyOnlineTradingCenter.ApplicationLayer.Concretions.Features.Users.UpdatePasswords.Commands.Update;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Validations.Users;

public class UpdatePasswordDtoValidator : AbstractValidator<UpdatePasswordCommandRequest>
{
    public UpdatePasswordDtoValidator()
    {

        RuleFor(dto => dto.UpdatePasswordDto.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(dto => dto.UpdatePasswordDto.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(dto => dto.UpdatePasswordDto.ResetToken)
            .NotEmpty().WithMessage("Reset token is required.");

        RuleFor(dto => dto.UpdatePasswordDto.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

        RuleFor(dto => dto.UpdatePasswordDto.ConfirmPassword)
            .NotEmpty().WithMessage("ConfirmPassword is required.")
            .MinimumLength(8).WithMessage("ConfirmPassword must be match to proceed.");
    }
}

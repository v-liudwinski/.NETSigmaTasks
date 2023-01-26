using FluentValidation;
using Homework17_LiudvynskyiV.S.Models.ViewModels;

namespace Homework17_LiudvynskyiV.S.Validators;

public class UserViewModelValidator : AbstractValidator<UserViewModel>
{
    public UserViewModelValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
                .WithMessage("Invalid name!");
        RuleFor(x => x.Surname)
            .NotNull()
            .NotEmpty()
                .WithMessage("Invalid surname!");
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
                .WithMessage("Invalid input!")
            .EmailAddress()
                .WithMessage("Valid email is required!");
    }
}
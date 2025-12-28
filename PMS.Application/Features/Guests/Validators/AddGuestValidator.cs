using System;
using FluentValidation;
using PMS.Application.Features.Guests.DTOs;

namespace PMS.Application.Features.Guests.Validators;

public class AddGuestValidator : AbstractValidator<AddGuestDto>
{

    public AddGuestValidator()
    {
        ApplyValidationRule();
        ApplyCustomValidationRule();

    }
    public void ApplyValidationRule()
    {
        RuleFor(g => g.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(g => g.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(g => g.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");

        RuleFor(g => g.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("A valid phone number is required.");

        RuleFor(g => g.IdNumber)
            .NotEmpty().WithMessage("ID Number is required.")
            .MaximumLength(20).WithMessage("ID Number cannot exceed 20 characters.");
        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Today)
            .When(x => x.DateOfBirth != default)
            .WithMessage("Date of birth must be in the past");

        RuleFor(x => x.EmergencyContactPhone)
            .Matches(@"^\+?[0-9]{8,15}$")
            .When(x => !string.IsNullOrEmpty(x.EmergencyContactPhone));

    }
    public void ApplyCustomValidationRule()
    {

    }
}

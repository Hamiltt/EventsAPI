using Common.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class RegisterParticipantRequestValidator : AbstractValidator<RegisterParticipantRequest>
    {
        public RegisterParticipantRequestValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("First name is required");

            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Last name is required");

            RuleFor(r => r.Username)
                .NotEmpty().WithMessage("Username is required");

            RuleFor(r => r.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required")
                .Must(BeAValidDate).WithMessage("Date of birth must be a valid date");

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default);
        }
    }
}
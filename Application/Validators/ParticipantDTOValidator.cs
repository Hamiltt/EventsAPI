using Common.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class ParticipantDTOValidator : AbstractValidator<ParticipantDTO>
    {
        public ParticipantDTOValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("First name is required");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("Last name is required");

            RuleFor(p => p.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required")
                .Must(BeAValidDate).WithMessage("Date of birth must be a valid date");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default);
        }
    }
}
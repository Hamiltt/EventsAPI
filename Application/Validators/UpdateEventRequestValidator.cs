using Common.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateEventRequestValidator : AbstractValidator<UpdateEventRequest>
    {
        public UpdateEventRequestValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Event name is required")
                .MaximumLength(100).WithMessage("Event name cannot exceed 100 characters");

            RuleFor(e => e.Description)
                .NotEmpty().WithMessage("Event description is required");

            RuleFor(e => e.Date)
                .GreaterThan(DateTime.Now).WithMessage("Event date must be in the future");

            RuleFor(e => e.Location)
                .NotEmpty().WithMessage("Event location is required");

            RuleFor(e => e.MaxParticipants)
                .GreaterThan(0).WithMessage("Max participants should be greater than zero");

            RuleFor(e => e.Category)
                .NotEmpty().WithMessage("Category is required");
        }
    }
}
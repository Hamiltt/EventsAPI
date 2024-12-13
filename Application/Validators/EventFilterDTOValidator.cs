using Common.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class EventFilterDTOValidator : AbstractValidator<EventFilterDTO>
    {
        public EventFilterDTOValidator()
        {
            RuleFor(f => f.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than zero");

            RuleFor(f => f.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than zero");
        }
    }
}
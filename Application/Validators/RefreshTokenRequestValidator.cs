using Common.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(r => r.AccessToken)
                .NotEmpty().WithMessage("Access token is required");

            RuleFor(r => r.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required");
        }
    }
}
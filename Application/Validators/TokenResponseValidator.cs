using Common.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class TokenResponseValidator : AbstractValidator<TokenResponse>
    {
        public TokenResponseValidator()
        {
            RuleFor(t => t.AccessToken)
                .NotEmpty().WithMessage("Access token is required");

            RuleFor(t => t.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required");
        }
    }
}
using Blazet.Application.Identity.DTOs;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Blazet.Application.Identity.Validators
{

    public class AppUserDtoValidator : AbstractValidator<AppUserDto>
    {
        private readonly IStringLocalizer<AppUserDtoValidator> _localizer;

        public AppUserDtoValidator(IStringLocalizer<AppUserDtoValidator> localizer)
        {
            _localizer = localizer;
            RuleFor(v => v.Provider)
                .MaximumLength(128).WithMessage(_localizer["Provider must be less than 100 characters"])
                .NotEmpty().WithMessage(_localizer["Provider cannot be empty"]);
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(_localizer["User name cannot be empty"])
                .Length(2, 100).WithMessage(_localizer["User name must be between 2 and 100 characters"]);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_localizer["E-mail cannot be empty"])
                .MaximumLength(100).WithMessage(_localizer["E-mail must be less than 100 characters"])
                .EmailAddress().WithMessage(_localizer["E-mail must be a valid email address"]);

            RuleFor(x => x.DisplayName)
                .MaximumLength(128).WithMessage(_localizer["Display name must be less than 128 characters"]);

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20).WithMessage(_localizer["Phone number must be less than 20 digits"]);
            _localizer = localizer;
        }
    }
}
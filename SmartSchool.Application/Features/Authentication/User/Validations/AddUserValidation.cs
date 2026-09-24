using FluentValidation;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Features.Authentication.User.Command;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Authentication.User.Validations
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public AddUserValidator(
            IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;

            // First Name
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .MaximumLength(100)
                .WithMessage(localizer[SharedResourcesKeys.MaxLength])
                .MinimumLength(2)
                .WithMessage(localizer[SharedResourcesKeys.MinLength])
                .WithName(localizer[SharedResourcesKeys.FirstName]);

            // Last Name
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .MaximumLength(100)
                .WithMessage(localizer[SharedResourcesKeys.MaxLength])
                .MinimumLength(2)
                .WithMessage(localizer[SharedResourcesKeys.MinLength])
                .WithName(localizer[SharedResourcesKeys.LastName]);

            // Email
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .EmailAddress()
                .WithMessage(localizer[SharedResourcesKeys.InvalidEmail])
                .MaximumLength(256)
                .WithMessage(localizer[SharedResourcesKeys.MaxLength])
                .WithName(localizer[SharedResourcesKeys.Email]);

            // Password
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .MinimumLength(8)
                .WithMessage(localizer[SharedResourcesKeys.MinLength])
                .MaximumLength(100)
                .WithMessage(localizer[SharedResourcesKeys.MaxLength])
                .Matches("[A-Z]")
                .WithMessage(localizer[SharedResourcesKeys.PasswordMustContainUppercase])
                .Matches("[a-z]")
                .WithMessage(localizer[SharedResourcesKeys.PasswordMustContainLowercase])
                .Matches("[0-9]")
                .WithMessage(localizer[SharedResourcesKeys.PasswordMustContainNumber])
                .Matches(@"[^a-zA-Z0-9]")
                .WithMessage(localizer[SharedResourcesKeys.PasswordMustContainSpecialCharacter])
                .WithName(localizer[SharedResourcesKeys.Password]);

            // Confirm Password
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .Equal(x => x.Password)
                .WithMessage(localizer[SharedResourcesKeys.PasswordMustContainSpecialCharacter])
                .WithName(localizer[SharedResourcesKeys.ConfirmPassword]);

            // Address - Optional
            RuleFor(x => x.Address)
                .MaximumLength(200)
                .WithMessage(localizer[SharedResourcesKeys.MaxLength])
                .MinimumLength(2)
                .WithMessage(localizer[SharedResourcesKeys.MinLength])
                .When(x => !string.IsNullOrWhiteSpace(x.Address))
                .WithName(localizer[SharedResourcesKeys.Address]);

            // Phone Number - Optional
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^01[0125][0-9]{8}$")
                .WithMessage(localizer[SharedResourcesKeys.InvalidEgyptianPhone])
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
                .WithName(localizer[SharedResourcesKeys.PhoneNumber]);
        }
    }
}

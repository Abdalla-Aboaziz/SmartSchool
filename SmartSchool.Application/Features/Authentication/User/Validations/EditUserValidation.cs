using FluentValidation;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Features.Authentication.User.Command;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Authentication.User.Validations
{
    public class EditUserValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100)
                .WithMessage(localizer[SharedResourcesKeys.InvalidEmail]);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100)
                .WithMessage(localizer[SharedResourcesKeys.InvalidEmail]);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage(localizer[SharedResourcesKeys.InvalidEmail]);

            RuleFor(x => x.Address)
                .MaximumLength(200)
                .When(x => !string.IsNullOrWhiteSpace(x.Address))
                .WithMessage(localizer[SharedResourcesKeys.MaxLength]);

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^01[0125][0-9]{8}$")
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
                .WithMessage(localizer[SharedResourcesKeys.InvalidEgyptianPhone]);
        }
    }
}

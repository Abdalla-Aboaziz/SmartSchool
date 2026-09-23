using FluentValidation;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Features.Subject.Commands;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Subject.Validations
{
    public class AddSubjectValidation : AbstractValidator<AddSubjectCommand>
    {
        public AddSubjectValidation(IStringLocalizer<SharedResources> localizer)
        {
            // Name
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .MaximumLength(200)
                .WithMessage(localizer[SharedResourcesKeys.MaxLength])
                .WithName(localizer[SharedResourcesKeys.Name]);

            // Period
            RuleFor(x => x.Period)
                .GreaterThan(0)
                .WithMessage(localizer[SharedResourcesKeys.GreaterThanZero])
                .WithName(localizer[SharedResourcesKeys.Period]);
        }
    }
}

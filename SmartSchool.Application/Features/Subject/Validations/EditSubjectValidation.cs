using FluentValidation;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Features.Subject.Commands;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Subject.Validations
{
    public class EditSubjectValidation : AbstractValidator<EditSubjectCommand>
    {
        public EditSubjectValidation(IStringLocalizer<SharedResources> localizer)
        {
            // Id
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required]);

            // Name
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .MaximumLength(200)
                .WithMessage(localizer[SharedResourcesKeys.MaxLength])
                .WithName(localizer[SharedResourcesKeys.Name])
                .When(x => x.Name is not null);

            // Period
            RuleFor(x => x.Period)
                .GreaterThan(0)
                .WithMessage(localizer[SharedResourcesKeys.GreaterThanZero])
                .WithName(localizer[SharedResourcesKeys.Period]);
        }
    }
}

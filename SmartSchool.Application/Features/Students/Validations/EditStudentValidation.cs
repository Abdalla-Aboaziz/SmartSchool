using FluentValidation;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Features.Students.Commands;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Students.Validations
{
    public class EditStudentValidation : AbstractValidator<EditStudentCommand>
    {
        public EditStudentValidation(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .MaximumLength(200)
                .WithMessage(localizer[SharedResourcesKeys.MaxLength])
                .WithName(localizer[SharedResourcesKeys.Name])
                .When(x => x.Name is not null);

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .MaximumLength(200)
                .WithMessage(localizer[SharedResourcesKeys.MaxLength])
                .WithName(localizer[SharedResourcesKeys.Address])
                .When(x => x.Address is not null);

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .Matches(@"^(010|011|012|015)\d{8}$")
                .WithMessage(localizer[SharedResourcesKeys.InvalidEgyptianPhone])
                .WithName(localizer[SharedResourcesKeys.InvalidEgyptianPhone])
                .When(x => x.Phone is not null);
        }
    }
}

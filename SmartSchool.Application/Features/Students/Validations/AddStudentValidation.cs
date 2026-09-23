using FluentValidation;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Features.Students.Commands;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Students.Validations
{
    public class AddDepartmentValidation : AbstractValidator<AddStudentCommand>
    {
        public AddDepartmentValidation(IStringLocalizer<SharedResources> localizer)
        {
            // Name
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .MaximumLength(200)
                .WithMessage(localizer[SharedResourcesKeys.MaxLength])
                .MinimumLength(2)
                .WithMessage(localizer[SharedResourcesKeys.MinLength])
                .WithName(localizer[SharedResourcesKeys.Name]);

            // Address
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .MaximumLength(200)
                .WithMessage(localizer[SharedResourcesKeys.MaxLength])
                .MinimumLength(3)
                .WithMessage(localizer[SharedResourcesKeys.MinLength])
                .WithName(localizer[SharedResourcesKeys.Address]);

            // Phone
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .Matches(@"^01[0125][0-9]{8}$")
                .WithMessage(localizer[SharedResourcesKeys.InvalidEgyptianPhone])
                .WithName(localizer[SharedResourcesKeys.Phone]);

            // Department
            RuleFor(x => x.DepartmentId)
                .GreaterThan(0)
                .WithMessage(localizer[SharedResourcesKeys.GreaterThanZero])
                .WithName(localizer[SharedResourcesKeys.DepartmentId]);
        }
    }
}


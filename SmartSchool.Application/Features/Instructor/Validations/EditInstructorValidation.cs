using FluentValidation;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Features.Instructor.Commands;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Instructor.Validations
{
    public class EditInstructorValidation : AbstractValidator<EditInstructorCommand>
    {
        public EditInstructorValidation(IStringLocalizer<SharedResources> localizer)
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

            // Address
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .MaximumLength(300)
                .WithMessage(localizer[SharedResourcesKeys.MaxLength])
                .WithName(localizer[SharedResourcesKeys.Address])
                .When(x => x.Address is not null);

            // Position
            RuleFor(x => x.Position)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .MaximumLength(100)
                .WithMessage(localizer[SharedResourcesKeys.MaxLength])
                .WithName(localizer[SharedResourcesKeys.Position])
                .When(x => x.Position is not null);

            // Salary
            RuleFor(x => x.Salary)
                .GreaterThan(0)
                .WithMessage(localizer[SharedResourcesKeys.GreaterThanZero])
                .WithName(localizer[SharedResourcesKeys.Salary]);

            // DepartmentId
            RuleFor(x => x.DepartmentId)
                .GreaterThan(0)
                .WithMessage(localizer[SharedResourcesKeys.GreaterThanZero])
                .WithName(localizer[SharedResourcesKeys.DepartmentId])
                .When(x => x.DepartmentId.HasValue);

            // SupervisorId
            RuleFor(x => x.SupervisorId)
                .GreaterThan(0)
                .WithMessage(localizer[SharedResourcesKeys.GreaterThanZero])
                .WithName(localizer[SharedResourcesKeys.SupervisorId])
                .When(x => x.SupervisorId.HasValue);
        }
    }
}

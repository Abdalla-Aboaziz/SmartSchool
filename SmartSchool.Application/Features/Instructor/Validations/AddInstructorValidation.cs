using FluentValidation;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Features.Instructor.Commands;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Instructor.Validations
{
    public class AddInstructorValidation : AbstractValidator<AddInstructorCommand>
    {
        public AddInstructorValidation(IStringLocalizer<SharedResources> localizer)
        {
            // Name
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .MaximumLength(200)
                .WithMessage(localizer[SharedResourcesKeys.MaxLength])
                .WithName(localizer[SharedResourcesKeys.Name]);

            // Address
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .MaximumLength(300)
                .WithMessage(localizer[SharedResourcesKeys.MaxLength])
                .WithName(localizer[SharedResourcesKeys.Address]);

            // Position
            RuleFor(x => x.Position)
                .NotEmpty()
                .WithMessage(localizer[SharedResourcesKeys.Required])
                .MaximumLength(100)
                .WithMessage(localizer[SharedResourcesKeys.MaxLength])
                .WithName(localizer[SharedResourcesKeys.Position]);

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

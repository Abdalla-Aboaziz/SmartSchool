using FluentValidation;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Features.Departments.Commands;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Features.Departments.Validations
{
    public class AddDepartmentValidation : AbstractValidator<AddDeparmentCommand>
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


        }
    }
}


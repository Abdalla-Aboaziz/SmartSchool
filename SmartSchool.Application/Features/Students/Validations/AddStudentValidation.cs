using FluentValidation;
using SmartSchool.Application.Features.Students.Commands;

namespace SmartSchool.Application.Features.Students.Validations
{


    public class AddStudentValidation : AbstractValidator<AddStudentCommand>
    {
        public AddStudentValidation()
        {
            // Name
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MaximumLength(200)
                .WithMessage("{PropertyName} cannot exceed 200 characters.")
                .MinimumLength(2)
                .WithMessage("{PropertyName} must be at least 2 characters.");


            // Address
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MaximumLength(200)
                .WithMessage("{PropertyName} cannot exceed 200 characters.")
                .MinimumLength(3)
                .WithMessage("{PropertyName} must be at least 3 characters.");

            // Phone
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .Matches(@"^01[0125][0-9]{8}$")
                .WithMessage("{PropertyName} must be a valid Egyptian mobile number.");

            // Department
            RuleFor(x => x.DepartmentId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}


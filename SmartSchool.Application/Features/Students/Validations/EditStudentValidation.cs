using FluentValidation;
using SmartSchool.Application.Features.Students.Commands;

namespace SmartSchool.Application.Features.Students.Validations
{
    public class EditStudentValidation : AbstractValidator<EditStudentCommand>
    {

        public EditStudentValidation()
        {

            RuleFor(x => x.Name)
                   .NotEmpty()
                   .MaximumLength(200)
                   .When(x => x.Name is not null);

            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(200)
                .When(x => x.Address is not null);

            RuleFor(x => x.Phone)
                .NotEmpty()
                .Matches(@"^(010|011|012|015)\d{8}$")
                .WithMessage("Phone number must be a valid Egyptian mobile number.")
                .When(x => x.Phone is not null);

        }
    }
}

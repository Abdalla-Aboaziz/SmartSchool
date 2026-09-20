using MediatR;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Students.Commands;

public class EditStudentCommandHandler
    : ResponseHandler,
      IRequestHandler<EditStudentCommand, Response<string>>
{
    private readonly IStudentRepository _studentRepository;

    public EditStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Response<string>> Handle(
     EditStudentCommand request,
     CancellationToken cancellationToken)
    {
        var existingStudent =
            await _studentRepository.GetByIdAsync(request.Id);

        if (existingStudent is null)
        {
            return NotFound<string>("Student not found.");
        }

        if (request.Name is not null)
        {

            existingStudent.UpdateName(request.Name);
        }

        if (request.Address is not null)
        {
            existingStudent.UpdateAddress(request.Address);
        }

        if (request.Phone is not null)
        {
            existingStudent.UpdatePhone(request.Phone);
        }

        if (request.DepartmentId.HasValue)
        {
            existingStudent.AssignToDepartment(request.DepartmentId.Value);
        }

        await _studentRepository.UpdateAsync(existingStudent);

        return Success<string>("Student updated successfully.");
    }
}
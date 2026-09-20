using MediatR;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;

namespace SmartSchool.Application.Features.Students.Commands
{
    public class DeleteStudentCommandHandler : ResponseHandler, IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        private readonly IStudentRepository _studentRepository;

        public DeleteStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id);

            if (student is null)
            {
                return NotFound<string>("Student not found.");
            }

            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                await trans.CommitAsync();
            }
            catch (Exception)
            {

                await trans.RollbackAsync();
                return BadRequest<string>("Failed to delete student.");
            }



            return Deleted<string>();
        }
    }
}

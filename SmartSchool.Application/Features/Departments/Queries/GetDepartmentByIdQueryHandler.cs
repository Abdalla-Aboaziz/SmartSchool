using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Application.Common;
using SmartSchool.Application.Features.Departments.Responses;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;
using SmartSchool.Domain.Entities;
using System.Linq.Expressions;

namespace SmartSchool.Application.Features.Departments.Queries
{
    public class GetDepartmentByIdQueryHandler : ResponseHandler, IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IStudentRepository _studentRepository;

        public GetDepartmentByIdQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IDepartmentRepository departmentRepository,
            IStudentRepository studentRepository) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _departmentRepository = departmentRepository;
            _studentRepository = studentRepository;
        }
        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var deparment = await _departmentRepository.GetDepartmentsQueryable()
                .Where(d => d.Id == request.DepartmentId)
                .Select(d => new GetDepartmentByIdResponse
                {
                    DepartmentId = d.Id,
                    DepartmentName = d.Name,
                    ManagerName = d.Manager != null ? d.Manager.Name : null,


                    //StudentList = d.Students.Select(s => new StudentResponse
                    //{
                    //    Id = s.Id,
                    //    Name = s.Name
                    //}).ToList(),



                    SubjectList = d.DepartmentSubjects.Select(s => new SubjectResponse
                    {
                        Id = s.SubjectId,
                        Name = s.Subject.Name
                    }).ToList(),
                    InstructorList = d.Instructors.Select(i => new InstructorResponse
                    {
                        Id = i.Id,
                        Name = i.Name
                    }).ToList()
                }).FirstOrDefaultAsync(cancellationToken);

            if (deparment == null)
            {
                return NotFound<GetDepartmentByIdResponse>(_stringLocalizer[SharedResourcesKeys.DepartmentNotFound]);
            }

            Expression<Func<Student, StudentResponse>> studentExpression = student => new StudentResponse
            {
                Id = student.Id,
                Name = student.Name
            };
            var studentsQuery = _studentRepository.GetStudentsQueryable()
                .Where(s => s.DepartmentId == request.DepartmentId)
                .Select(studentExpression);

            var paginatedStudents = await studentsQuery
                .ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);

            deparment.StudentList = paginatedStudents;

            return Success(deparment);

        }
    }
};



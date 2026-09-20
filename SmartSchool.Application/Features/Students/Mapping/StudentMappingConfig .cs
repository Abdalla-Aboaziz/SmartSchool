using Mapster;
using SmartSchool.Application.Features.Students.Responses;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Features.Students.Mapping
{
    public class StudentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Student, GetStudentsListResponse>()
                .Map(dest => dest.StudentId, src => src.Id)
                .Map(dest => dest.DepartmentName, src => src.Department!.Name);

            config.NewConfig<Student, GetStudentByIdResponse>()
                .Map(dest => dest.StudentId, src => src.Id)
                .Map(dest => dest.DepartmentName, src => src.Department!.Name);

            //     config.NewConfig<AddStudentCommand, Student>();
            //config.NewConfig<EditStudentCommand, Student>()
            //    .Map(dest => dest.Id, src => src.Id).Ignore();


        }
    }
}

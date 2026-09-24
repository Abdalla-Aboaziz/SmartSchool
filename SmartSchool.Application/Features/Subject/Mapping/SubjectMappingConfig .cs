using Mapster;
using SmartSchool.Application.Features.Subject.Responses;

namespace SmartSchool.Application.Features.Subject.Mapping
{
    public class SubjectMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<SmartSchool.Domain.Entities.Subject, GetSubjectByIdResponse>()
                .Map(dest => dest.StudentCount, src => src.StudentsSubjects.Count)
                .Map(dest => dest.DepartmentCount, src => src.DepartmentSubjects.Count)
                .Map(dest => dest.InstructorCount, src => src.InstructorSubjects.Count);
        }
    }
}

using Mapster;
using SmartSchool.Application.Features.Departments.Responses;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Features.Departments.Mapping
{
    public class DepartmentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Department, GetDepartmentsListResponse>()
                .Map(dest => dest.DepartmentId, src => src.Id)
                .Map(dest => dest.DepartmentName, src => src.Name)
                .Map(dest => dest.StudentCount, src => src.Students.Count);




        }
    }
}

using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Abstractions.Persistence.Repositories
{
    public interface IDepartmentRepository : IGenericRepositoryAsync<Department>
    {

        IQueryable<Department> GetDepartmentsQueryable();
        Task<List<Department>> GetDepartmentsListAsync();
        Task<bool> IsDepartmentExist(int key);
    }

}

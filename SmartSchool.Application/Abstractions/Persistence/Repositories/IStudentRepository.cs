using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Abstractions.Persistence.Repositories
{
    public interface IStudentRepository : IGenericRepositoryAsync<Student>
    {
        IQueryable<Student> GetStudentsQueryable();
        Task<List<Student>> GetStudentsListAsync();
        Task<Student?> GetByIdWithDepartmentAsync(int id);

    }
}
using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Abstractions.Persistence.Repositories
{
    public interface IStudentRepository : IGenericRepositoryAsync<Student>
    {
        public Task<List<Student>> GetStudentsListAsync();
        Task<Student?> GetByIdWithDepartmentAsync(int id);

    }
}
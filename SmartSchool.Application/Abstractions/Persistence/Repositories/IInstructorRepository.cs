using SmartSchool.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSchool.Application.Abstractions.Persistence.Repositories
{
    public interface IInstructorRepository : IGenericRepositoryAsync<Instructor>
    {
        Task<List<Instructor>> GetListAsync();
    }
}

using SmartSchool.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSchool.Application.Abstractions.Persistence.Repositories
{
    public interface ISubjectRepsitory : IGenericRepositoryAsync<Subject>
    {
        Task<List<Subject>> GetListAsync();
    }
}

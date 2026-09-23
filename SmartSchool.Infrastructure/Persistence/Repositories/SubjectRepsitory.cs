using Microsoft.EntityFrameworkCore;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Domain.Entities;
using SmartSchool.Infrastructure.Persistence.Data;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SmartSchool.Infrastructure.Persistence.Repositories
{
    public class SubjectRepsitory : GenericRepositoryAsync<Subject>, ISubjectRepsitory
    {
        private readonly DbSet<Subject> _subjects;
        public SubjectRepsitory(ApplicationDbContext dbContext) : base(dbContext)
        {
            _subjects = dbContext.Set<Subject>();
        }

        public async Task<List<Subject>> GetListAsync()
            => await _subjects.AsNoTracking().ToListAsync();
    }
}
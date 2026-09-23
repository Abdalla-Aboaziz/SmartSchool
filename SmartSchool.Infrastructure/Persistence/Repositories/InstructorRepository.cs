using Microsoft.EntityFrameworkCore;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Domain.Entities;
using SmartSchool.Infrastructure.Persistence.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSchool.Infrastructure.Persistence.Repositories
{
    public class InstructorRepository : GenericRepositoryAsync<Instructor>, IInstructorRepository
    {
        private readonly DbSet<Instructor> _instructors;
        public InstructorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _instructors = dbContext.Set<Instructor>();
        }

        public async Task<List<Instructor>> GetListAsync()
            => await _instructors.AsNoTracking().ToListAsync();
    }
}

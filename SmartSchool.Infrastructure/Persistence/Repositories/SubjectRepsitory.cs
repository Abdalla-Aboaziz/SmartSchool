using Microsoft.EntityFrameworkCore;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Domain.Entities;
using SmartSchool.Infrastructure.Persistence.Data;


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
            => await _subjects
        .AsNoTracking()
        .AsNoTracking()
        .Include(s => s.StudentsSubjects)
        .Include(s => s.DepartmentSubjects)
        .Include(s => s.InstructorSubjects)
        .ToListAsync();
    }
}
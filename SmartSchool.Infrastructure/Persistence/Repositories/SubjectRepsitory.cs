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




    }
}

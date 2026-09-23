using Microsoft.EntityFrameworkCore;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Domain.Entities;
using SmartSchool.Infrastructure.Persistence.Data;

namespace SmartSchool.Infrastructure.Persistence.Repositories
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {
        private readonly DbSet<Department> _departments;

        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _departments = dbContext.Set<Department>();
        }


        public async Task<List<Department>> GetDepartmentsListAsync()
        => await _departments
            .AsNoTracking().Include(s => s.Students)
            .ToListAsync();

        public IQueryable<Department> GetDepartmentsQueryable()
        {
            return _departments.AsNoTracking();
        }


    }
}

using Microsoft.EntityFrameworkCore;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Domain.Entities;

using SmartSchool.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Infrastructure.Persistence.Repositories
{
    public  class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
    {
        private readonly DbSet<Student> _students;

        public StudentRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _students = dbContext.Set<Student>();
        }

        public async Task<Student?> GetByIdWithDepartmentAsync(int id)
            =>  await _students.AsNoTracking().Include(s => s.Department).FirstOrDefaultAsync(s => s.Id == id);




        public async Task<List<Student>> GetStudentsListAsync()
            => await _students.AsNoTracking().Include(s => s.Department).ToListAsync();
       
    }
}

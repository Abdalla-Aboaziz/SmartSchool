
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Application.Abstractions.Persistence.Repositories;
using SmartSchool.Infrastructure.Persistence.Data;
using SmartSchool.Infrastructure.Persistence.Repositories;

namespace SmartSchool.Infrastructure
{
    public static class InfrastructureDependancyInjection
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services,
        IConfiguration configuration)
        {


            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IInstructorRepository, InstructorRepository>();
            services.AddScoped<ISubjectRepsitory, SubjectRepsitory>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));


            // Identity 
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                //  options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;

                //DefaultValues
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.AllowedForNewUsers = true;
            });

            return services;
        }
    }


}

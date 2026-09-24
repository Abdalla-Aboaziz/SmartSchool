using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Application.PipleLineBehaivors;

using System.Reflection;

namespace SmartSchool.Application
{
    public static class ApplicationDependancyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {

            services.AddMediatR(opt => opt.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

            services.AddMapsterConfig();
            services.AddValidationConfig();
            return services;
        }

        private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
        {
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton<IMapper>(new Mapper(mappingConfig));

            return services;
        }
        private static IServiceCollection AddValidationConfig(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }

    }
}

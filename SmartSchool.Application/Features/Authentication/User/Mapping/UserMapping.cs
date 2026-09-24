using Mapster;
using SmartSchool.Application.Features.Authentication.User.Command;
using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Features.Authentication.User.Mapping
{
    internal class UserMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {

            config.NewConfig<AddUserCommand, ApplicationUser>()
           .Map(dest => dest.UserName, src => src.Email)
           .Map(dest => dest.EmailConfirmed, src => true);


        }
    }
}

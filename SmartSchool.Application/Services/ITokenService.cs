using SmartSchool.Domain.Entities;

namespace SmartSchool.Application.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(ApplicationUser user);
        Domain.Entities.RefreshToken GenerateRefreshToken();
    }
}

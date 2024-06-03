using Microsoft.AspNetCore.Identity;

namespace PFA_ProjectAPI.Infrastructure.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}

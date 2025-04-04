using e_school.Models;

namespace e_school.Services
{
    public interface ITokenService
    {
        Task<string> GenerateAccessJwtToken(User user);
    }
}

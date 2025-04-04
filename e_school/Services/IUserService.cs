using e_school.DTOs;
using Microsoft.AspNetCore.Identity;

namespace e_school.Services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync (RegisterDTO model);
        Task<string> LoginAsync(LoginDTO model);
    }
}

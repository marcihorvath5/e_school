using e_school.DTOs;
using Microsoft.AspNetCore.Identity;

namespace e_school.Services
{
    public interface IUserService
    {
        public Task<IdentityResult> RegisterAsync (RegisterDTO model);
        public Task<IdentityResult> Login();
    }
}

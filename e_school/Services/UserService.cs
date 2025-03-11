using e_school.DTOs;
using e_school.Models;
using Microsoft.AspNetCore.Identity;

namespace e_school.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDTO model)
        {
            var existingUser =await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError {Description = $"A(z) {model.Email} mátr hozzá van rendelve egy felhasználóhoz"});
            }

            User newUser = new User()
            {
                Email = model.Email,
                UserName = model.Email,
                BirthDate = model.BirthDate
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);
            return result;
        }
        public Task<IdentityResult> Login()
        {
            throw new NotImplementedException();
        }
    }
}

using e_school.DTOs;
using e_school.Models;
using Microsoft.AspNetCore.Identity;
using Swashbuckle.Swagger;

namespace e_school.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
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
        public async Task<string> LoginAsync(LoginDTO model)
        {
            User user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return null;
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            

            if (!result.Succeeded)
            {
                return "Hibás jelszó";
            }

            string token = await _tokenService.GenerateAccessJwtToken(user);

            return token;
        }
    }
}

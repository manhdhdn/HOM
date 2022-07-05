using HOM.Data.Context;
using HOM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HOM.Repository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly HOMContext _context;

        public AccountRepo(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            HOMContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            var user = new ApplicationUser()
            {
                Name = signUpModel.Name,
                UserName = signUpModel.Phone,
                PhoneNumber = signUpModel.Phone
            };

            var role = await _context.Roles.FindAsync(signUpModel.RoleId);

            if (role == null /*|| role.Name.Equals("Admin")*/)
                return IdentityResult.Failed();

            var result = await _userManager.CreateAsync(user, signUpModel.Password);

            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, role.Name);
            }

            return result;
        }

        public async Task<string?> SignInAsync(SignInModel signInModel)
        {
            var results = await _signInManager.PasswordSignInAsync(signInModel.Phone, signInModel.Password, false, false);

            if (!results.Succeeded)
            {
                return null;
            }

            var user = _userManager.FindByNameAsync(signInModel.Phone).Result;

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, signInModel.Phone),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var roleName = _userManager.GetRolesAsync(user).Result.FirstOrDefault();

            if (roleName != null)
                authClaims.Add(new Claim(ClaimTypes.Role, roleName));

            var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
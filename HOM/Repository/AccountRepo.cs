using HOM.Data;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public AccountRepo(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            DataContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            var user = new ApplicationUser()
            {
                Phone = signUpModel.Phone,
                UserName = signUpModel.Phone
            };

            var role = _context.Roles.Find(signUpModel.RoleId);

            if (role == null || role.Name.Equals("Admin"))
                return IdentityResult.Failed();

            var roleExit = await _roleManager.RoleExistsAsync(role.Name);
            if (!roleExit) await _roleManager.CreateAsync(new IdentityRole(role.Name));
            await _userManager.CreateAsync(user, signUpModel.Password);
            return await _userManager.AddToRoleAsync(user, role.Name);
        }

        public async Task<string?> SignInAsync(SignInModel signInModel)
        {
            var results = await _signInManager.PasswordSignInAsync(signInModel.Phone, signInModel.Password, false, false);

            if (!results.Succeeded)
            {
                return null;
            }

            var user = _userManager.FindByNameAsync(signInModel.Phone).Result;
            string roleName = _userManager.GetRolesAsync(user).Result.First<string>();

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, signInModel.Phone),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };


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
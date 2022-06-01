using HOM.Data;
using HOM.Models;
using HOM.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepo _accountRepo;
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsController(IAccountRepo accountRepo, DataContext context, UserManager<ApplicationUser> userManager)
        {
            _accountRepo = accountRepo;
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            var result = await _accountRepo.SignUpAsync(signUpModel);

            if (result.Succeeded)
            {
                var user = _userManager.FindByNameAsync(signUpModel.Phone).Result;
                var account = new Accounts()
                {
                    Name = signUpModel.Name,
                    Phone = signUpModel.Phone,
                    PasswordHash = _userManager.PasswordHasher.HashPassword(user, signUpModel.Password),
                    Gender = signUpModel.Gender,
                    RoleId = signUpModel.RoleId,
                    Avatar = signUpModel.Avatar,
                    Status = true
                };
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                return Ok(result.Succeeded);
            }

            return Unauthorized();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            var token = await _accountRepo.SignInAsync(signInModel);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            var accounts = (from account in _context.Accounts
                            join role in _context.Roles on account.RoleId equals role.Id
                            where account.Status == true && account.Phone == signInModel.Phone
                            select new AccountModel()
                            {
                                Id = account.Id,
                                Name = account.Name,
                                Phone = account.Phone,
                                Gender = account.Gender,
                                RoleName = role.Name,
                                Avatar = account.Avatar
                            }).ToList();
            
            if (accounts.Count == 0)
            {
                return Ok(accounts);
            }

            var result = new
            {
                accounts,
                token
            };

            return Ok(result);
        }
    }
}

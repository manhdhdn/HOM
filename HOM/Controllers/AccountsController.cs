using HOM.Data;
using HOM.Data.Context;
using HOM.Models;
using HOM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HOM.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepo _accountRepo;
        private readonly HOMContext _context;

        public AccountsController(IAccountRepo accountRepo, HOMContext context)
        {
            _accountRepo = accountRepo;
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ActionResult<PagedModel<Account>>> GetAccounts(int pageIndex, int pageSize, [Required] int roleId, string? hostelId, string? roomId)
        {
            if (_context.Accounts == null || roleId == 1)
            {
                return NotFound();
            }

            var source = _context.Accounts.Where(a => a.RoleId == roleId);

            if (hostelId != null)
            {
                source = source.Join(_context.RoomMemberships
                    .Join(_context.Rooms.Where(r => r.HostelId == hostelId),
                    roomMember => roomMember.RoomId, 
                    room => room.Id,
                    (roomMember, room) => new RoomMembership()),
                    account => account.Id,
                    roomMembers => roomMembers.AccountId,
                    (account, roomMember) => new Account());
            }

            if (roomId != null)
            {
                source = source.Join(_context.RoomMemberships.Where(r => r.RoomId == roomId),
                    account => account.Id,
                    roomMembers => roomMembers.AccountId,
                    (account, roomMember) => new Account());
            }

            return await PaginatedList<Account>.CreateAsync(source, pageIndex, pageSize);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Owner")]
        public async Task<ActionResult<Account>> GetAccount(string id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            var result = await _accountRepo.SignUpAsync(signUpModel);

            if (result.Succeeded)
            {
                _context.Accounts.Add(new Account(signUpModel));
                await _context.SaveChangesAsync();

                return await SignIn(new SignInModel() { Phone = signUpModel.Phone, Password = signUpModel.Password });
            }

            var error = result.Errors.FirstOrDefault();

            return ValidationProblem(ExceptionHandle.Handle(new Exception(error == null ? "" : error.Description), signUpModel.GetType(), ModelState));
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var token = await _accountRepo.SignInAsync(signInModel);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            var accounts = _context.Accounts.FirstOrDefault(a => a.Phone == signInModel.Phone);

            return Ok(new { accounts, token });
        }
    }
}

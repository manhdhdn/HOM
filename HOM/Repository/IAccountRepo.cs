using HOM.Models;
using Microsoft.AspNetCore.Identity;

namespace HOM.Repository
{
    public interface IAccountRepo
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        Task<string?> SignInAsync(SignInModel signInModel);
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using FoodDeliveryProject.Model.Models;

namespace FoodDeliveryProject.Services.UserValidation
{
    public interface IApplicationUserSignInService
    {
        Task<SignInStatus> Login(string email, string password, bool rememberMe);
        Task<bool> VerifyCodeGet();
        Task<SignInStatus> VerifyCodePost(string provider, string code, bool isPersistent, bool rememberBrowser);
        String GetVerifiedUserId();
        Task<bool> SendTwoFactorCode(string provider);
        void SignIn(ApplicationUser user);
    }
}

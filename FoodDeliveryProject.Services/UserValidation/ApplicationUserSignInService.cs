using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using FoodDeliveryProject.DataAccess.UnitOfWork;
using FoodDeliveryProject.Model.Models;

namespace FoodDeliveryProject.Services.UserValidation
{
    public class ApplicationUserSignInService : IApplicationUserSignInService
    {
        private IUnitOfWork _unitOfWork;

        public ApplicationUserSignInService(IUnitOfWork uof)
        {
            _unitOfWork = uof;
        }

        public string GetVerifiedUserId()
        {
            return _unitOfWork.UserRepository.SignInManager.GetVerifiedUserId();
        }

        public async Task<SignInStatus> Login(string email, string password, bool rememberMe)
        {
            return await _unitOfWork.UserRepository.SignInManager.PasswordSignInAsync(email, password, rememberMe, shouldLockout: false);
        }

        public async Task<bool> SendTwoFactorCode(string provider)
        {
            return await _unitOfWork.UserRepository.SignInManager.SendTwoFactorCodeAsync(provider);
        }

        public void SignIn(ApplicationUser user)
        {
            _unitOfWork.UserRepository.SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
        }

        public async Task<bool> VerifyCodeGet()
        {
            return await _unitOfWork.UserRepository.SignInManager.HasBeenVerifiedAsync();
        }

        public async Task<SignInStatus> VerifyCodePost(string provider, string code, bool isPersistent, bool rememberBrowser)
        {
            return await _unitOfWork.UserRepository.SignInManager.TwoFactorSignInAsync(provider, code, isPersistent, rememberBrowser);
        }
    }
}
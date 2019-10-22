using FoodDeliveryProject.DataAccess.UnitOfWork;
using FoodDeliveryProject.Model.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryProject.Services.UserValidation
{
    public class ApplicationUserRegisterService : IApplicationUserRegisterService
    {
        private IUnitOfWork _unitOfWork;

        public ApplicationUserRegisterService(IUnitOfWork uof)
        {
            _unitOfWork = uof;
        }

        public async Task<bool> CanResetPassword(string email)
        {
            var user = await _unitOfWork.UserRepository.UserManager.FindByEmailAsync(email);

            return await _unitOfWork.UserRepository.UserManager.IsEmailConfirmedAsync(user.Id) ? true : false;
        }

        public async Task<bool> ConfirmEMail(string id, string code)
        {
            var identityResult = await _unitOfWork.UserRepository.UserManager.ConfirmEmailAsync(id, code);

            return identityResult.Succeeded;
        }

        public async Task<IList<string>> GetValidTwoFactorProviders(string id)
        {
            return await _unitOfWork.UserRepository.UserManager.GetValidTwoFactorProvidersAsync(id);
        }

        public async Task<bool> RegisterApplicationUser(ApplicationUser user, string password)
        {
            var identityResult = await _unitOfWork.UserRepository.UserManager.CreateAsync(user, password);
            _unitOfWork.UserRepository.UserManager.AddToRole(user.Id, "Customer");

            return identityResult.Succeeded;
        }

        public async Task<bool> ResetPassword(string email, string code, string password)
        {
            if (await CanResetPassword(email))
            {
                return _unitOfWork.UserRepository.UserManager.ResetPassword(email, code, password).Succeeded;
            }

            return false;
        }
    }
}

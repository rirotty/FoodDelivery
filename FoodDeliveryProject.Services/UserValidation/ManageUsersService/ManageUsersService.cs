using FoodDeliveryProject.DataAccess.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodDeliveryProject.Services.UserValidation.Enums;
using Microsoft.AspNet.Identity;
using System.Security.Principal;

namespace FoodDeliveryProject.Services.UserValidation.ManageUsersService
{
    public class ManageUsersService : IManageUsersServise
    {
        private IUnitOfWork _unitOfWork;

        public ManageUsersService(IUnitOfWork uof)
        {
            _unitOfWork = uof;
        }

        public async Task AddPhoneNumber(string id, string phoneNumber)
        {
            await _unitOfWork.UserRepository.UserManager.GenerateChangePhoneNumberTokenAsync(id, phoneNumber);

            return;
        }

        public async Task<ManageMessageId> ChangePassword(string id, string oldPassword, string newPassword)
        {
            var result = await _unitOfWork.UserRepository.UserManager.ChangePasswordAsync(id, oldPassword, newPassword);
            if (result.Succeeded)
            {
                var user = await _unitOfWork.UserRepository.UserManager.FindByIdAsync(id);
                if (user != null)
                {
                    await _unitOfWork.UserRepository.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return ManageMessageId.ChangePasswordSuccess;
            }
            return ManageMessageId.Error;
        }

        public async Task DisableTwoFactorAuthentification(string id)
        {
            await _unitOfWork.UserRepository.UserManager.SetTwoFactorEnabledAsync(id, false);
            var user = await _unitOfWork.UserRepository.UserManager.FindByIdAsync(id);
            if (user != null)
            {
                await _unitOfWork.UserRepository.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
        }

        public async Task EnableTwoFactorAuthentification(string id)
        {
            await _unitOfWork.UserRepository.UserManager.SetTwoFactorEnabledAsync(id, true);
            var user = await _unitOfWork.UserRepository.UserManager.FindByIdAsync(id);
            if (user != null)
            {
                await _unitOfWork.UserRepository.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
        }

        public async Task<IList<UserLoginInfo>> GetInternalLogins(string id)
        {
            IList<UserLoginInfo> userLogins = null;
            var user = await _unitOfWork.UserRepository.UserManager.FindByIdAsync(id);
            if (user != null)
            {
                userLogins = await _unitOfWork.UserRepository.UserManager.GetLoginsAsync(id);
            }
            return userLogins;
        }

        public async Task<string> GetPhoneNumber(string id)
        {
            string phoneNumber = await _unitOfWork.UserRepository.UserManager.GetPhoneNumberAsync(id);

            return phoneNumber;
        }

        public bool HasPassword(string id)
        {
            return _unitOfWork.UserRepository.UserManager.HasPassword(id);
        }

        public async Task<bool> HasTwoFactorEnabled(string id)
        {
            return await _unitOfWork.UserRepository.UserManager.GetTwoFactorEnabledAsync(id);
        }

        public async Task<ManageMessageId> RemoveLogin(IPrincipal userController, UserLoginInfo userLoginInfo)
        {
            ManageMessageId message;
            var result = await _unitOfWork.UserRepository.UserManager.RemoveLoginAsync(userController.Identity.GetUserId(), userLoginInfo);
            if (result.Succeeded)
            {
                var user = await _unitOfWork.UserRepository.UserManager.FindByIdAsync(userController.Identity.GetUserId());
                if (user != null)
                {
                    await _unitOfWork.UserRepository.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return message;
        }

        public async Task<ManageMessageId> RemovePhoneNumber(string id)
        {
            var result = await _unitOfWork.UserRepository.UserManager.SetPhoneNumberAsync(id, null);
            if (!result.Succeeded)
            {
                return ManageMessageId.Error;
            }
            var user = await _unitOfWork.UserRepository.UserManager.FindByIdAsync(id);
            if (user != null)
            {
                await _unitOfWork.UserRepository.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return ManageMessageId.RemovePhoneSuccess;
        }

        public async Task<ManageMessageId> SetPassword(string id, string newPassword)
        {
            var result = await _unitOfWork.UserRepository.UserManager.AddPasswordAsync(id, newPassword);
            if (result.Succeeded)
            {
                var user = await _unitOfWork.UserRepository.UserManager.FindByIdAsync(id);
                if (user != null)
                {
                    await _unitOfWork.UserRepository.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return ManageMessageId.SetPasswordSuccess;
            }
            return ManageMessageId.Error;
        }
    }
}

using FoodDeliveryProject.Services.UserValidation.Enums;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FoodDeliveryProject.Services.UserValidation.ManageUsersService
{
    public interface IManageUsersServise
    {
        Task<ManageMessageId> RemoveLogin(IPrincipal user, UserLoginInfo userLoginInfo);
        Task AddPhoneNumber(string id, string phoneNumber);
        Task EnableTwoFactorAuthentification(string id);
        Task DisableTwoFactorAuthentification(string id);
        Task<ManageMessageId> RemovePhoneNumber(string id);
        Task<ManageMessageId> ChangePassword(string id, string oldPassword, string newPassword);
        Task<ManageMessageId> SetPassword(string id, string newPassword);
        Task<IList<UserLoginInfo>> GetInternalLogins(string id);
        bool HasPassword(string id);
        Task<string> GetPhoneNumber(string id);
        Task<bool> HasTwoFactorEnabled(string id);

    }
}

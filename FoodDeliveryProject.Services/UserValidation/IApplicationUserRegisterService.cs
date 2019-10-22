using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryProject.Services.UserValidation
{
    public interface IApplicationUserRegisterService
    {
        Task<bool> RegisterApplicationUser(ApplicationUser user, string password);
        Task<bool> ConfirmEMail(string id, string code);
        Task<bool> CanResetPassword(string email);
        Task<bool> ResetPassword(string email, string code, string password);
        Task<IList<string>> GetValidTwoFactorProviders(string id);
    }
}

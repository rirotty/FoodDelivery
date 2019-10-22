using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository.ApplicationUserManagement;
using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces
{
    public interface IUserRepository
    {
        ApplicationUserManager UserManager { get; }
        ApplicationSignInManager SignInManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IEnumerable<ApplicationUser> ReadAll();
        Address GetUsersAddress(string id);
    }
}

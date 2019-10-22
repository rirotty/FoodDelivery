using FoodDeliveryProject.Services.Administration.UsersRequestsInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryProject.Services.Administration
{
    public interface IAdministrationService : IRoleRequestsService, IRestaurantRequestsService
    {
        IEnumerable<string> GetAllRoles();
        Task<IEnumerable<string>> GetAllNonUserRoles(string userName);
        Task AddRole(string roleName);
        Task RemoveRole(string roleName);
    }
}

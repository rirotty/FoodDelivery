using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryProject.Services.Administration.UsersRequestsInterfaces
{
    public interface IRoleRequestsService
    {
        IEnumerable<UsersRoleRequest> ListAllRoleRequests();
        Task GenerateRoleRequest(string userName, string role, string documentationPath);
        void RemoveRoleRequest(int id);
        Task ApproveRoleRequest(int id);
        void DeclineRoleRequest(int id);
    }
}

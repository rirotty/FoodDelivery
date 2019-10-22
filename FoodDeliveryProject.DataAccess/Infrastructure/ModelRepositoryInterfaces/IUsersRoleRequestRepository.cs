using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces
{
    public interface IUsersRoleRequestRepository : IRepository<UsersRoleRequest>
    {
        IEnumerable<UsersRoleRequest> GetRequests();
        IEnumerable<UsersRoleRequest> GetRequests(string role);
        IEnumerable<UsersRoleRequest> GetPendingRequests();
        int FindIdByUserId(string userId);
        UsersRoleRequest FindByUserId(string userId);
        UsersRoleRequest FindPopulatedById(int id);
    }
}

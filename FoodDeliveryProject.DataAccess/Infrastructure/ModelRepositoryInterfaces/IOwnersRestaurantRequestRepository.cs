using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces
{
    public interface IOwnersRestaurantRequestRepository : IRepository<OwnersRestaurantRequest>
    {
        IEnumerable<OwnersRestaurantRequest> GetRequests();
        IEnumerable<OwnersRestaurantRequest> GetPendingRequests();
        int FindIdByUserId(string userId);
        OwnersRestaurantRequest FindByUserId(string userId);
        OwnersRestaurantRequest FindPopulatedById(int id);
    }
}

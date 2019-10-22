using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryProject.Services.Administration.UsersRequestsInterfaces
{
    public interface IRestaurantRequestsService
    {
        IEnumerable<OwnersRestaurantRequest> ListAllRequestsForRestaurants();
        Task GenerateRestaurantRequest(string username, string restaurantId, string documentationPath);
        void RemoveRestaurantRequest(int id);
        void ApproveRestaurantRequest(int id);
        void DeclineRestaurantRequest(int id);
    }
}

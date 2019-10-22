using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        int GetIdByName(string restaurantName);
        IEnumerable<Restaurant> GetRestaurantsInCity(string city);
    }
}

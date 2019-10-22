using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetRestaurantCategories(int restaurantId);
    }
}

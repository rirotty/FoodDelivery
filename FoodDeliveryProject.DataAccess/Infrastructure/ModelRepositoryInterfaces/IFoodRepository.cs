using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces
{
    public interface IFoodRepository : IRepository<Food>
    {
        IEnumerable<Food> GetSubcategoryFood(int subcategoryId);
        bool FoodExists(int foodId, int billId);
    }
}

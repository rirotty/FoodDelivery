using FoodDeliveryProject.Model.Models;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetExistingOrderInCurrentBillByFoodID(int billId, int foodId);
    }
}

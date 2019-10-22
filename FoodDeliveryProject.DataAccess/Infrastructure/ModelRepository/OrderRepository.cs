using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using System.Data.Entity;
using System.Linq;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext dbContext) : base(dbContext){ }

        public FoodDeliveryDbContext DatabaseContext
        {
            get { return _context as FoodDeliveryDbContext; }
        }

        public Order GetExistingOrderInCurrentBillByFoodID(int billId, int foodId)
        {
            Order orderQuery =  (from bill in DatabaseContext.BillSet
                                join order in DatabaseContext.OrderSet on bill.Id equals order.BillId
                                join food in DatabaseContext.OrderSet on order.FoodId equals food.Id
                                where (bill.Id == billId) && (food.Id == foodId) && (bill.BillStatus == Model.Enumerations.BillStatus.Pending)
                                select order).FirstOrDefault();

            return orderQuery;
        }
    }
}

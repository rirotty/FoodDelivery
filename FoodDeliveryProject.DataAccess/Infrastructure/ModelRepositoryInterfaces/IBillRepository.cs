using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces
{
    public interface IBillRepository : IRepository<Bill>
    {
        Bill GetCustomersPendingBill(string customerId);
    }
}

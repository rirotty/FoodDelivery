using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces
{
    public interface IDeliveryRepository : IRepository<Delivery>
    {
        IEnumerable<Delivery> GetAllCustomersPreviousDeliveries(string customerId);
        IEnumerable<Delivery> GetAllCustomersDeliveriesInProgress(string customerId);
    }
}

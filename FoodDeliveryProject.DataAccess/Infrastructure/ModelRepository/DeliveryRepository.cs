using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    class DeliveryRepository : Repository<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(DbContext dbContext) : base(dbContext){ }

        public FoodDeliveryDbContext DatabaseContext
        {
            get { return _context as FoodDeliveryDbContext; }
        }

        public IEnumerable<Delivery> GetAllCustomersDeliveriesInProgress(string customerId)
        {
            var deliveries = from delivery in DatabaseContext.DeliverySet
                             join bills in DatabaseContext.BillSet on delivery.Id equals bills.DeliveryId
                             where (delivery.DeliveryStatus != Model.Enumerations.DeliveryStatus.Delivered) && (bills.CustomerId == customerId)
                             select delivery;

            return deliveries.ToList();
        }

        public IEnumerable<Delivery> GetAllCustomersPreviousDeliveries(string customerId)
        {
            var deliveries = from delivery in DatabaseContext.DeliverySet
                             join bills in DatabaseContext.BillSet on delivery.Id equals bills.DeliveryId
                             where (delivery.DeliveryStatus == Model.Enumerations.DeliveryStatus.Delivered) && (bills.CustomerId == customerId)
                             select delivery;

            return deliveries.ToList();
        }
    }
}

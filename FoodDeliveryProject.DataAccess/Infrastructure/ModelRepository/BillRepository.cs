using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    public class BillRepository : Repository<Bill>, IBillRepository
    {
        public BillRepository(DbContext dbContext) : base(dbContext){ }

        public FoodDeliveryDbContext DatabaseContext
        {
            get { return _context as FoodDeliveryDbContext; }
        }

        public Bill GetCustomersPendingBill(string customerId)
        {
            var billsQuery = (from bills in DatabaseContext.BillSet
                        where (bills.BillStatus == Model.Enumerations.BillStatus.Pending) && (bills.CustomerId == customerId)
                        select bills).FirstOrDefault();

            return billsQuery;
        }
    }
}

using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using System.Data.Entity;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(DbContext dbContext) : base(dbContext) { }
    }
}

using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using System.Data.Entity;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    public class BusinessHoursRepository : Repository<BusinessHours>, IBusinessHoursRepository
    {
        public BusinessHoursRepository(DbContext dbContext) : base(dbContext){ }
    }
}

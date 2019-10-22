using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using System.Data.Entity;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    class VacationDaysRepository : Repository<VacationDays>, IVacationDaysRepository
    {
        public VacationDaysRepository(DbContext dbContext) : base(dbContext){ }
    }
}

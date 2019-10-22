using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using System.Data.Entity;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext dbContext) : base(dbContext) { }
    }
}

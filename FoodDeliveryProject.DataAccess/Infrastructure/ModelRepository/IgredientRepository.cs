using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    public class IgredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        public IgredientRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}

using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using System.Data.Entity;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(DbContext dbContext) : base(dbContext){ }
    }
}

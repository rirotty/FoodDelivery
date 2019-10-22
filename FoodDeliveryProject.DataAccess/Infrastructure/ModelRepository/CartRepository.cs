using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(DbContext dbContext) : base(dbContext){ }
    }
}

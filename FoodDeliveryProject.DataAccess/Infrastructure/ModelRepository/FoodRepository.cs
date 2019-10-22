using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    public class FoodRepository : Repository<Food>, IFoodRepository
    {
        public FoodRepository(DbContext dbContext) : base(dbContext){ }

        public FoodDeliveryDbContext DatabaseContext
        {
            get { return _context as FoodDeliveryDbContext; }
        }

        public bool FoodExists(int foodId, int billId)
        {
            var foods = from bill in DatabaseContext.BillSet
                        join order in DatabaseContext.OrderSet on bill.Id equals order.BillId
                        join food in DatabaseContext.FoodSet on order.FoodId equals food.Id
                        where (bill.Id == billId) && (food.Id == foodId)
                        select food;

            int count = foods.Count();
            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerable<Food> GetSubcategoryFood(int subcategoryId)
        {
            IEnumerable<Food> foodsQuery = from subcategory in DatabaseContext.SubcategorySet
                        join food in DatabaseContext.FoodSet
                        on subcategory.Id equals food.SubcategoryId
                        where (food.SubcategoryId == subcategoryId) && (food.FoodStatus == Model.Enumerations.FoodStatus.Available)
                        select food;

            return foodsQuery.ToList();
        }
    }
}

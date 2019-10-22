using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext dbContext) : base(dbContext){ }

        public FoodDeliveryDbContext DatabaseContext
        {
            get { return _context as FoodDeliveryDbContext; }
        }

        public IEnumerable<Category> GetRestaurantCategories(int restaurantId)
        {
            var categories = from category in DatabaseContext.CategorySet
                             join subcategory in DatabaseContext.SubcategorySet
                             on category.Id equals subcategory.CategoryId
                             join food in DatabaseContext.FoodSet
                             on subcategory.Id equals food.SubcategoryId
                             where food.RestaurantId == restaurantId
                             select category;

            return categories.ToList();
        }
    }
}

using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using System.Data.Entity;
using FoodDeliveryProject.Model;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    class SubcategoryRepository : Repository<Subcategory>, ISubcategoryRepository
    {
        public SubcategoryRepository(DbContext dbContext) : base(dbContext){ }

        public FoodDeliveryDbContext DatabaseContext
        {
            get { return _context as FoodDeliveryDbContext; }
        }

        public IEnumerable<Subcategory> GetCategorySubcategories(int categoryId)
        {
            IEnumerable<Subcategory> subCategories = from category in DatabaseContext.CategorySet
                                join subcategory in DatabaseContext.SubcategorySet
                                on category.Id equals subcategory.CategoryId
                                where subcategory.CategoryId == categoryId
                                select subcategory;

            return subCategories.ToList();
        }
    }
}

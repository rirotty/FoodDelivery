using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces
{
    public interface ISubcategoryRepository : IRepository<Subcategory>
    {
        IEnumerable<Subcategory> GetCategorySubcategories(int categoryId);
    }
}

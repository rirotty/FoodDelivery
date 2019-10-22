using FoodDeliveryProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryProject.Services.RestaurantService
{
    public interface IRestaurantService
    {
        void AddCategory(Category category);
        void AddSubcategory(Subcategory subcategory);
        void AddFood(Food food);
        void EditRestaurantInfo(Restaurant restaurant);
        void EditCategory(Category category);
        void EditSubcategory(Subcategory subcategory);
        void EditFood(Food food);
        IEnumerable<Delivery> GetRestaurantDeliveries(int restaurantId);
        IEnumerable<Food> GetRestaurantFood(int restaurantId);
        void ChangeDeliveryStatus(Delivery delivery);
    }
}

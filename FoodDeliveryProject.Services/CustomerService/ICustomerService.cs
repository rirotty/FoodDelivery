using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;

namespace FoodDeliveryProject.Services.CustomerService
{
    public interface ICustomerService
    {
        IEnumerable<Restaurant> GetRestaurantsInCity(string city);
        IEnumerable<Food> GetSubcategoryFood(int subcategoryId);
        IEnumerable<Delivery> ViewCurrentDeliveries(string customerId);
        Bill GetPendingBill(string customerId);
        Food GetFoodById(int foodId);
        ApplicationUser GetUserById(string userId);
        IEnumerable<Order> GetAllOrdersInBill(int billId);
        Order GetOrderById(int orderId);
        List<Restaurant> GetAllRestaurants();
        List<Category> GetCategories(int restaurantId);
        List<Subcategory> GetSubcategories(int categoryId);
        bool AddOrder(int quantity, int foodId, string userId);
        IEnumerable<Order> UpdateFoodQuantity(int foodId, int quantity, string userId);
        IEnumerable<Order> RemoveOrder(int orderId, string userId);
        void CreateDelivery(Address address, string paymentMethod, string userId);
        Address GetUserAddress(string userId);
    }
}

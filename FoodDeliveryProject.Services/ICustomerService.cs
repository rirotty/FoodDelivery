using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;

namespace FoodDeliveryProject.Services.CustomerService
{
    public interface ICustomerService
    {
        ApplicationUser GetCurrentUserById(string id);
        IEnumerable<Restaurant> GetRestaurantsInCity(string city);
        IEnumerable<Category> GetRestaurantCategories(int restaurantId);
        IEnumerable<Subcategory> GetCategorySubCategories(int categoryId);
        IEnumerable<Food> GetSubcategoryFood(int subcategoryId);
        IEnumerable<Delivery> GetAllPreviousDeliveries(string customerId);
        IEnumerable<Delivery> ViewCurrentDeliveries(string customerId);
        void AddOrder(Order order);
        void AddBill(Bill bill);
        void AddDelivery(Delivery delivery);
        void CancelDelivery(Delivery delivery);
        void UpdateOrder(Order order);
        void UpdateBill(Bill bill);
        void DeleteOrder(Order order);
        void DeleteBill(Bill bill);
        bool PendingBill(string customerId);
        Bill GetPendingBill(string customerId);
        bool FoodExists(int foodId, int billId);
        Food GetFood(int foodId);
        Order GetExistingOrderInCurrentBillByFoodID(int billId, int foodId);
        Food GetFoodById(int foodId);
        ApplicationUser GetUserById(string userId);
        IEnumerable<Order> GetAllOrdersInBill(int billId);
        Order GetOrderById(int orderId);
        void AddPayment(Payment payment);

    }
}

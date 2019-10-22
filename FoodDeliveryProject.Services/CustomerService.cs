using FoodDeliveryProject.DataAccess.UnitOfWork;
using FoodDeliveryProject.Model.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryProject.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ApplicationUser GetCurrentUserById(string id)
        {
            return _unitOfWork.UserRepository.UserManager.FindById(id);
        }
        //choosing restaurant methods
        public IEnumerable<Restaurant> GetRestaurantsInCity(string city)
        {
            return _unitOfWork.RestaurantRepository.Get(x => x.RestaurantAddress.City == city);
            // var restaurants = from restaurant in _unitOfWork.RestaurantRepository.ReadAll()
            //  where restaurant.RestaurantAddress.City.Contains(city)
            // select restaurant;
            //  return restaurants;
        }
        //displaying menu methods
        public IEnumerable<Category> GetRestaurantCategories(int restaurantId)
        {
            var categories = from category in _unitOfWork.CategoryRepository.ReadAll()
                             join subcategory in _unitOfWork.SubcategoryRepository.ReadAll()
                             on category.Id equals subcategory.CategoryId
                             join food in _unitOfWork.FoodRepository.ReadAll()
                             on subcategory.Id equals food.SubcategoryId
                             where food.RestaurantId == restaurantId
                             select category;
            return categories;
        }
        public IEnumerable<Subcategory> GetCategorySubCategories(int categoryId)
        {
            var subCategories = from category in _unitOfWork.CategoryRepository.ReadAll()
                                join subcategory in _unitOfWork.SubcategoryRepository.ReadAll()
                                on category.Id equals subcategory.CategoryId
                                where subcategory.CategoryId == categoryId
                                select subcategory;
            return subCategories;
        }
        public IEnumerable<Food> GetSubcategoryFood(int subcategoryId)
        {
            var Foods = from subcategory in _unitOfWork.SubcategoryRepository.ReadAll()
                        join food in _unitOfWork.FoodRepository.ReadAll()
                        on subcategory.Id equals food.SubcategoryId
                        where (food.SubcategoryId == subcategoryId) && (food.FoodStatus == Model.Enumerations.FoodStatus.Available)
                        select food;
            return Foods;
        }
        //history of deliveries
        public IEnumerable<Delivery> GetAllPreviousDeliveries(string customerId)
        {
            var deliveries = from delivery in _unitOfWork.DeliveryRepository.ReadAll()
                             join bills in _unitOfWork.BillRepository.ReadAll() on delivery.Id equals bills.DeliveryId

                             where (delivery.DeliveryStatus == Model.Enumerations.DeliveryStatus.Delivered) && (bills.CustomerId == customerId)
                             select delivery;
            return deliveries;
        }
        //checking for pending bill
        public bool PendingBill(string customerId)
        {
            var bills = from bill in _unitOfWork.BillRepository.ReadAll()
                        where (bill.BillStatus == Model.Enumerations.BillStatus.Pending) && (bill.CustomerId == customerId)
                        select bill;
            int count = bills.Count();
            if (count == 0)
            { return false; }
            else { return true; }
        }
        public Bill GetPendingBill(string customerId)
        {
            var bills = from bill in _unitOfWork.BillRepository.ReadAll()
                        where (bill.BillStatus == Model.Enumerations.BillStatus.Pending) && (bill.CustomerId == customerId)
                        select bill;
            if (bills.Count() != 0)
            {
                Bill pendingBill = bills.First();
                return pendingBill;
            }
            else { return null; }
        }

        //current delivery state
        public IEnumerable<Delivery> ViewCurrentDeliveries(string customerId)
        {
            var deliveries = from delivery in _unitOfWork.DeliveryRepository.ReadAll()
                             join bills in _unitOfWork.BillRepository.ReadAll() on delivery.Id equals bills.DeliveryId

                             where (delivery.DeliveryStatus != Model.Enumerations.DeliveryStatus.Delivered) && (bills.CustomerId == customerId)
                             select delivery;
            return deliveries;
        }
        //order management methods
        public void AddOrder(Order order)
        {
            _unitOfWork.OrderRepository.Add(order);
            _unitOfWork.Commit();

        }
        public void AddBill(Bill bill)
        {
            _unitOfWork.BillRepository.Add(bill);
            _unitOfWork.Commit();

        }
        public void AddDelivery(Delivery delivery)
        {
            _unitOfWork.DeliveryRepository.Add(delivery);
            _unitOfWork.Commit();

        }
        public void CancelDelivery(Delivery delivery)
        {
            _unitOfWork.DeliveryRepository.Edit(delivery);
            _unitOfWork.Commit();
        }
        public void UpdateOrder(Order order)
        {
            _unitOfWork.OrderRepository.Edit(order);
            _unitOfWork.Commit();
        }
        public void UpdateBill(Bill bill)
        {
            _unitOfWork.BillRepository.Edit(bill);
            _unitOfWork.Commit();
        }
        public void DeleteOrder(Order order)
        {
            _unitOfWork.OrderRepository.Delete(order);
            _unitOfWork.Commit();
        }
        public void DeleteBill(Bill bill)
        {
            _unitOfWork.BillRepository.Delete(bill);
            _unitOfWork.Commit();
        }
        //Checking if food is already in Bill

        public bool FoodExists(int foodId, int billId)
        {
            var foods = from bill in _unitOfWork.BillRepository.ReadAll()
                        join order in _unitOfWork.OrderRepository.ReadAll() on bill.Id equals order.Id
                        join food in _unitOfWork.FoodRepository.ReadAll() on order.FoodId equals food.Id
                        where (bill.Id == billId) && (food.Id == foodId)
                        select food;
            int count = foods.Count();
            if (count == 0)
            { return false; }
            else { return true; }
        }
        public Food GetFood(int foodId)
        {
            Food food = _unitOfWork.FoodRepository.GetById(foodId);
            return food;
        }
        public Order GetExistingOrderInCurrentBillByFoodID(int billId, int foodId)
        {

            var orders = from bill in _unitOfWork.BillRepository.ReadAll()
                         join order in _unitOfWork.OrderRepository.ReadAll() on bill.Id equals order.BillId
                         join food in _unitOfWork.FoodRepository.ReadAll() on order.FoodId equals food.Id
                         where (bill.Id == billId) && (food.Id == foodId)&&(bill.BillStatus==Model.Enumerations.BillStatus.Pending)
                         select order;
            Order existingOrder = orders.First();
            return existingOrder;
        }
        public Food GetFoodById(int foodId)
        {
            return _unitOfWork.FoodRepository.GetById(foodId);
        }
        public ApplicationUser GetUserById(string userId)
        {
            return _unitOfWork.UserRepository.UserManager.FindById(userId);
        }
        public IEnumerable<Order> GetAllOrdersInBill(int billId)
        {
            return _unitOfWork.OrderRepository.Get(x => x.BillId == billId);
        }
        public Order GetOrderById(int orderId)
        {
            return _unitOfWork.OrderRepository.GetById(orderId);
        }
        public void AddPayment(Payment payment)
        {
            _unitOfWork.PaymentRepository.Add(payment);
            _unitOfWork.Commit();

        }
    }
}

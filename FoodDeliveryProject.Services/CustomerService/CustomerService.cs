using FoodDeliveryProject.DataAccess.UnitOfWork;
using FoodDeliveryProject.Model.Models;
using Microsoft.AspNet.Identity;
using System;
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

        //choosing restaurant methods
        public IEnumerable<Restaurant> GetRestaurantsInCity(string city)
        {
            return _unitOfWork.RestaurantRepository.GetRestaurantsInCity(city);
        }

        public IEnumerable<Food> GetSubcategoryFood(int subcategoryId)
        {
            return _unitOfWork.FoodRepository.GetSubcategoryFood(subcategoryId);
        }
        //checking for pending bill
        private bool PendingBill(string customerId)
        {
            Bill bill = _unitOfWork.BillRepository.GetCustomersPendingBill(customerId);

            if (bill == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Food GetFoodById(int foodId)
        {
            return _unitOfWork.FoodRepository.GetById(foodId);
        }

        public IEnumerable<Order> GetAllOrdersInBill(int billId)
        {
            return _unitOfWork.OrderRepository.Get(x => x.BillId == billId);
        }

        public Order GetOrderById(int orderId)
        {
            return _unitOfWork.OrderRepository.GetById(orderId);
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return _unitOfWork.RestaurantRepository.Get().ToList();
        }

        public List<Category> GetCategories(int restaurantId)
        {
            return _unitOfWork.CategoryRepository.GetRestaurantCategories(restaurantId).ToList();
        }

        public List<Subcategory> GetSubcategories(int categoryId)
        {
            return _unitOfWork.SubcategoryRepository.GetCategorySubcategories(categoryId).ToList();
        }

        public bool AddOrder(int quantity, int foodId, string userId)
        {
            float total = 0;
            ICollection<Order> orders = new List<Order>();

            if (this.PendingBill(userId) == false)
            {
                Order order = new Order()
                {
                    FoodId = foodId,
                    Quantity = quantity,
                    Food = this.GetFoodById(foodId)
                };
                orders.Add(order);
                Bill bill = new Bill()
                {
                    BillStatus = Model.Enumerations.BillStatus.Pending,
                    CustomerId = userId,
                    BillDate = DateTime.Now,
                    Orders = orders,
                };

                List<Order> billOrders = this.GetAllOrdersInBill(bill.Id).ToList();
                for (int i = 0; i < billOrders.Count(); i++)
                {
                    total = total + billOrders[i].Food.FoodPrice * billOrders[i].Quantity;
                }

                bill.TotalPrice = total;
                order.Bill = bill;
                _unitOfWork.OrderRepository.Add(order);
                _unitOfWork.BillRepository.Edit(bill);
                _unitOfWork.Commit();
            }
            else
            {
                Bill bill = _unitOfWork.BillRepository.GetCustomersPendingBill(userId);
                if (_unitOfWork.FoodRepository.FoodExists(foodId, bill.Id) == true)
                {
                    Order order = _unitOfWork.OrderRepository.GetExistingOrderInCurrentBillByFoodID(bill.Id, foodId);
                    order.Quantity = quantity;
                    List<Order> billOrders = this.GetAllOrdersInBill(bill.Id).ToList();
                    for (int i = 0; i < billOrders.Count(); i++)
                    {
                        total = total + billOrders[i].Food.FoodPrice * billOrders[i].Quantity;
                    }
                    bill.TotalPrice = total;
                    _unitOfWork.OrderRepository.Edit(order);
                    _unitOfWork.BillRepository.Edit(bill);
                    _unitOfWork.Commit();
                }
                else
                {
                    Order order = new Order()
                    {
                        FoodId = foodId,
                        Quantity = quantity
                    };
                    order.BillId = bill.Id;
                    _unitOfWork.OrderRepository.Add(order);
                    bill.BillDate = DateTime.Now;
                    bill.Orders.Add(order);
                    List<Order> billOrders = this.GetAllOrdersInBill(bill.Id).ToList();
                    for (int i = 0; i < billOrders.Count(); i++)
                    {
                        total = total + billOrders[i].Food.FoodPrice * billOrders[i].Quantity;
                    }
                    bill.TotalPrice = total;
                    _unitOfWork.BillRepository.Edit(bill);
                    _unitOfWork.Commit();
                }
            }
            return true;
        }

        public IEnumerable<Order> UpdateFoodQuantity(int foodId, int quantity, string userId)
        {
            float total = 0;
            Bill bill = _unitOfWork.BillRepository.GetCustomersPendingBill(userId);
            Order order = _unitOfWork.OrderRepository.GetExistingOrderInCurrentBillByFoodID(bill.Id, foodId);
            order.Quantity = quantity;
            List<Order> billOrders = this.GetAllOrdersInBill(bill.Id).ToList();
            for (int i = 0; i < billOrders.Count(); i++)
            {
                total = total + billOrders[i].Food.FoodPrice * billOrders[i].Quantity;
            }
            bill.TotalPrice = total;
            _unitOfWork.OrderRepository.Edit(order);
            _unitOfWork.BillRepository.Edit(bill);
            _unitOfWork.Commit();

            return this.GetAllOrdersInBill(bill.Id).ToList();
        }

        public IEnumerable<Order> RemoveOrder(int orderId, string userId)
        {
            float total = 0;
            Bill bill = _unitOfWork.BillRepository.GetCustomersPendingBill(userId);
            Order order = this.GetOrderById(orderId);
            bill.Orders.Remove(order);
            _unitOfWork.OrderRepository.Delete(order);
            List<Order> billOrders = this.GetAllOrdersInBill(bill.Id).ToList();
            for (int i = 0; i < billOrders.Count(); i++)
            {
                total = total + billOrders[i].Food.FoodPrice * billOrders[i].Quantity;
            }
            bill.TotalPrice = total;
            _unitOfWork.BillRepository.Edit(bill);
            _unitOfWork.Commit();

            return _unitOfWork.OrderRepository.Get(x => x.BillId == bill.Id).ToList();
        }

        public void CreateDelivery(Address address, string paymentMethod, string userId)
        {
            DateTime paymentDate;
            DateTime defaultDate = DateTime.MinValue;
            Bill bill = _unitOfWork.BillRepository.GetCustomersPendingBill(userId);

            if (paymentMethod == "Credit Card") { paymentDate = DateTime.Now; }
            else { paymentDate = defaultDate; }
            Payment payment = new Payment()
            {
                PaymentMethod = paymentMethod,
                Status = Model.Enumerations.PaymentStatus.Pending,
                PaymentDate = paymentDate
            };
            Delivery delivery = new Delivery()
            {
                Payment = payment,
                DeliveryArrival = defaultDate,
                DeliveryDeparture = defaultDate,
                SpecifiedAddress = address,
                DeliveryStatus = Model.Enumerations.DeliveryStatus.WaitingForApproval,
            };
            bill.BillStatus = Model.Enumerations.BillStatus.Submitted;
            delivery.Bills = new List<Bill>();
            delivery.Bills.Add(bill);
            _unitOfWork.DeliveryRepository.Add(delivery);
            _unitOfWork.Commit();
        }

        public Address GetUserAddress(string userId)
        {
            return _unitOfWork.UserRepository.GetUsersAddress(userId);
        }

        public IEnumerable<Delivery> ViewCurrentDeliveries(string customerId)
        {
            return _unitOfWork.DeliveryRepository.GetAllCustomersDeliveriesInProgress(customerId);
        }

        public Bill GetPendingBill(string customerId)
        {
            return _unitOfWork.BillRepository.GetCustomersPendingBill(customerId);
        }

        public ApplicationUser GetUserById(string userId)
        {
            return _unitOfWork.UserRepository.UserManager.FindById(userId);
        }
    }
}

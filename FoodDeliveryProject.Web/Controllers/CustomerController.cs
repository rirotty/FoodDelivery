using FoodDeliveryProject.Model.Models;
using FoodDeliveryProject.Services.CustomerService;
using FoodDeliveryProject.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FoodDeliveryProject.Web.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: Customer
        public ActionResult Index(RestaurantSearchViewModel searchParameters)
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            if (searchParameters == null)
            {
                restaurants = _customerService.GetAllRestaurants().ToList();
            }
            else
            {
                restaurants = _customerService.GetRestaurantsInCity(searchParameters.City).ToList();
            }

            return View(restaurants);
        }

        public ActionResult ListCategories(int restaurantId)
        {
           List<Category> categories = _customerService.GetCategories(restaurantId);
            return View("Categories", categories);
        }

        public ActionResult ListSubcategories(int categoryId)
        {
            List<Subcategory> subCategories = _customerService.GetSubcategories(categoryId);
            return View("Subcategories", subCategories);
        }

        public ActionResult ListFood(int subCategoryId)
        {
            TempData["subcategoryId"] = subCategoryId;
            IEnumerable<Food> food = _customerService.GetSubcategoryFood(subCategoryId);
            return View("Food", food);
        }

        public ActionResult AddOrderToBill(string quantity, string foodId)
        {
            string currentUserId = User.Identity.GetUserId();

            if (!String.IsNullOrEmpty(quantity) && !String.IsNullOrEmpty(foodId))
            {
                _customerService.AddOrder(Int32.Parse(quantity), Int32.Parse(foodId), currentUserId);
            }

            int subcategoryId = (int)TempData["subcategoryId"];
            
            IEnumerable<Food> food = _customerService.GetSubcategoryFood((subcategoryId));
            return View("Food", food);
        }

        public ActionResult CustomerCart()
        {
            string currentUserId = User.Identity.GetUserId();
            Bill bill = _customerService.GetPendingBill(currentUserId);
            IEnumerable<Order> cart = _customerService.GetAllOrdersInBill(bill.Id).ToList();

            return View("Cart", cart);
        }

        public ActionResult UpdateOrderQuantity(string foodId, string quantity)
        {
            IEnumerable<Order> cart = new List<Order>();
            if (!String.IsNullOrEmpty(quantity) && !String.IsNullOrEmpty(foodId))
            {
                cart = _customerService.UpdateFoodQuantity(int.Parse(foodId), int.Parse(quantity), User.Identity.GetUserId()).ToList();
            }

            return View("Cart", cart);
        }

        public ActionResult RemoveOrder(int? orderId)
        {
            List<Order> cart = new List<Order>();

            if (orderId.HasValue)
            {
                cart = _customerService.RemoveOrder(orderId.Value, User.Identity.GetUserId()).ToList();
            }

            return View("Cart", cart);
        }

        public ActionResult SubmitDeliveryForm()
        {
            return View();
        }

        public ActionResult InitiateDelivery(DeliveryViewModel delivery)
        {
            string currentUserId = User.Identity.GetUserId();
            Address specifiedAddress;
            if (delivery != null)
            {
                if (delivery.MyAddress != "true")
                {
                    specifiedAddress = new Address()
                    {
                        ApartmentNumber = int.Parse(delivery.Address.ApartmentNumber),
                        City = delivery.Address.City,
                        Country = delivery.Address.Country,
                        Floor = int.Parse(delivery.Address.Floor),
                        PostalCode = delivery.Address.PostalCode,
                        Province = delivery.Address.Province,
                        StreetName = delivery.Address.StreetName,
                        StreetNumber = int.Parse(delivery.Address.StreetNumber)
                    };

                    _customerService.CreateDelivery(specifiedAddress, delivery.PaymentMethod, currentUserId);
                }
                else
                {
                    specifiedAddress = _customerService.GetUserAddress(User.Identity.GetUserId());

                    _customerService.CreateDelivery(specifiedAddress, delivery.PaymentMethod, currentUserId);
                }
               
            }

            IEnumerable<Delivery> currentDeliveries = _customerService.ViewCurrentDeliveries(currentUserId);
            return View("CurrentDeliveries", currentDeliveries);
        }





    }
}
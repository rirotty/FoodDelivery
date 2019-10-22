using FoodDeliveryProject.Model.Models;
using FoodDeliveryProject.Services.RestaurantService;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Linq;
using FoodDeliveryProject.Services.Administration;
using FoodDeliveryProject.Model.Enumerations;
using System.Collections;

namespace FoodDeliveryProject.Web.Controllers
{
    public class RestaurantController : Controller
    {
        private IRestaurantService _restaurantService;
        private IAdministrationService _administrationService;
        public RestaurantController(IRestaurantService restaurantService, IAdministrationService administrationService)
        {
            _restaurantService = restaurantService;
            _administrationService = administrationService;
        }

        // GET: Restaurant
        public ActionResult Index()
        {
            ApplicationUser currentUser = _restaurantService.GetCurrentUserById(User.Identity.GetUserId());
            return View();
        }

        public ActionResult ListRestaurants()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = _restaurantService.GetCurrentUserById(currentUserId);

            IEnumerable<Restaurant> restaurants = _restaurantService.GetRestaurantsByOwnerId(currentUserId);
            return View("Restaurants", restaurants);
        }

        public ActionResult AddRestaurantForm()
        {
            RestaurantViewModel restaurantVM = new RestaurantViewModel();
            return View(restaurantVM);
        }

        [HttpPost]
        public ActionResult AddRestaurant(RestaurantViewModel restaurantVM)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = _restaurantService.GetCurrentUserById(currentUserId);

            _administrationService.GenerateRestaurantRequest(currentUser.UserName, restaurantVM.RestaurantName, "");
            _restaurantService.AddRestaurant(restaurantVM, currentUserId);
            Restaurant restaurantFiltered = _restaurantService.GetRestaurantByFilter(x => x.RestaurantName == restaurantVM.RestaurantName && x.UserId == currentUserId);
            return View("AddWorkingHoursForm", restaurantFiltered);
        }

        public ActionResult AddWorkingHours(string restaurantId, string mondayOpeningHours, string tuesdayOpeningHours, string wednesdayOpeningHours, string thursdayOpeningHours, string fridayOpeningHours,
            string saturdayOpeningHours, string sundayOpeningHours, string mondayClosingHours, string tuesdayClosingHours, string wednesdayClosingHours, string thursdayClosingHours, string fridayClosingHours,
            string saturdayClosingHours, string sundayClosingHours)
        {
            int rId = int.Parse(restaurantId);
            Restaurant restaurant = _restaurantService.GetRestaurantByFilter(x => x.Id == rId);
            _restaurantService.AddBusinessHours(restaurant, mondayOpeningHours, mondayClosingHours, tuesdayOpeningHours, tuesdayClosingHours, wednesdayOpeningHours, wednesdayClosingHours,
                thursdayOpeningHours, thursdayClosingHours, fridayOpeningHours, fridayClosingHours, saturdayOpeningHours, saturdayClosingHours, sundayOpeningHours, sundayClosingHours);
            return RedirectToAction("ListRestaurants");
        }

        public ActionResult ListCategories()
        {
            IEnumerable<Category> categories = _restaurantService.GetAllCategories();
            return View("Categories", categories);
        }

        public ActionResult AddCategoryForm()
        {
            return View();
        }

        public ActionResult AddCategory(string name, string description)
        {
            Category category = new Category() { CategoryName = name, CategoryDescription = description };
            _restaurantService.AddCategory(category);
            return View("Categories", _restaurantService.GetAllCategories());
        }

        public ActionResult ListSubcategories(string id)
        {
            IEnumerable<Subcategory> subcategories = _restaurantService.GetSubcategoriesByCategoryId(int.Parse(id));
            TempData["categoryId"] = id;
            return View("Subcategories", subcategories);
        }

        public ActionResult AddSubcategoryForm()
        {
            return View();
        }

        public ActionResult AddSubcategory(string name, string description)
        {
            string categoryId = (string)TempData["categoryId"];
            Subcategory subcategory = new Subcategory() { SubcategoryName = name, SubcategoryDescription = description };
            subcategory.Category = _restaurantService.GetCategoryById(int.Parse(categoryId));
            _restaurantService.AddSubcategory(subcategory);
            return View("Subcategories", _restaurantService.GetSubcategoriesByCategoryId(int.Parse(categoryId)));
        }

        public ActionResult ListRestaurantFood(string id)
        {
            IEnumerable<Food> food = _restaurantService.GetAllRestaurantFood(int.Parse(id));

            ViewBag.showAddToRestaurant = "";
            return View("Food", food);
        }

        public ActionResult ListFood(string subcategoryId)
        {
            IEnumerable<Food> food = _restaurantService.GetFoodBySubcategoryId(int.Parse(subcategoryId));
            TempData["subcategoryId"] = subcategoryId;

            return View("Food", food);
        }

        public ActionResult AddFoodForm()
        {
            return View();
        }

        public ActionResult AddFood(string name, string description, string price, string status, string type)
        {
            string subcategoryId = (string)TempData["subcategoryId"];
            Food food = _restaurantService.AddFood(name, description, price, status, type, subcategoryId);
            if (type == "standard")
            {
                return View("Food", _restaurantService.GetFoodBySubcategoryId(int.Parse(subcategoryId)));
            }
            TempData["food"] = food;
            return View("Ingredients", food.Ingredients);
        }

        public ActionResult AddFoodToRestaurantForm(string foodId)
        {
            TempData["foodId"] = foodId;
            return View();
        }

        public ActionResult AddFoodToRestaurant(string restaurantName, string[] addToAll)
        {
            string foodId = (string)TempData["foodId"];
            int id = int.Parse(foodId);
            Food foodToAdd = _restaurantService.GetFoodByFilter(x => x.Id == id);
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = _restaurantService.GetCurrentUserById(currentUserId);

            bool retVal = _restaurantService.AddFoodToRestaurant(addToAll, currentUserId, foodToAdd, restaurantName);
            if (!retVal)
            {
                return View("FoodExists");
            }

            return View("Food", _restaurantService.GetFoodBySubcategoryId(foodToAdd.SubcategoryId));
        }

        public ActionResult EditFoodForm(string foodId)
        {
            int idFood = int.Parse(foodId);
            TempData["foodId"] = idFood;
            Food food = _restaurantService.GetFoodByFilter(x => x.Id == idFood);
            IEnumerable<Ingredient> ingredients = _restaurantService.GetIngredientsByFilter(x => x.FoodId == food.Id);
            food.Ingredients = ingredients;
            return View(food);
        }

        public ActionResult EditFood(string name, string description, string price, string status, string type)
        {
            int foodId = (int)TempData["foodId"];
            Food foodToEdit = _restaurantService.GetFoodByFilter(x => x.Id == foodId);
            _restaurantService.EditFood(foodToEdit, name, description, price, status, type);
            IEnumerable<Food> food = _restaurantService.GetAllRestaurantFood((int)foodToEdit.RestaurantId);
            ViewBag.showAddToRestaurant = "";
            return View("Food", food);
        }

        public ActionResult ListIngredients(string foodId, string restaurantId)
        {
            int idFood = int.Parse(foodId);
            int idRestaurant = int.Parse(restaurantId);
            //TempData["food"] = _restaurantService.GetFoodById(idFood);
            IEnumerable<Ingredient> ingredients = _restaurantService.GetIngredientsByFilter(x => x.FoodId == idFood && x.Food.RestaurantId == idRestaurant);
            return View("Ingredients", ingredients);
        }

        public ActionResult AddIngredientForm(string foodId)
        {
            TempData["foodId"] = int.Parse(foodId);
            return View();
        }

        public ActionResult AddIngredient(string name, string price, string description)
        {
            int foodId = (int)TempData["foodId"];
            Food food = _restaurantService.GetFoodByFilter(x => x.Id == foodId);
            _restaurantService.AddIngredient(food, name, price, description);
            TempData["food"] = food;
            return View("Ingredients", _restaurantService.GetIngredientsByFilter(x => x.FoodId == food.Id));
        }

        public ActionResult EditIngredientForm(string ingredientId)
        {
            int id = int.Parse(ingredientId);
            TempData["ingredientId"] = id;
            Ingredient ingredient = _restaurantService.GetIngredientsByFilter(x => x.Id == id).FirstOrDefault();
            return View(ingredient);
        }

        public ActionResult EditIngredient(string name, string price, string description)
        {
            int ingredientId = (int)TempData["ingredientId"];
            Ingredient ingredientToEdit = _restaurantService.GetIngredientsByFilter(x => x.Id == ingredientId).FirstOrDefault();
            _restaurantService.EditIngredient(ingredientToEdit, name, price, description);
            int foodId = (int)ingredientToEdit.FoodId;
            Food food = _restaurantService.GetFoodByFilter(x => x.Id == foodId);
            food.Ingredients = _restaurantService.GetIngredientsByFilter(x => x.FoodId == food.Id && x.Food.RestaurantId == food.RestaurantId);
            return View("EditFoodForm", food);
        }

        public ActionResult RemoveIngredient(string ingredientId)
        {
            Food food = _restaurantService.RemoveIngredient(ingredientId);
            return View("EditFoodForm", food);
        }

        public ActionResult ListDeliveries(string id)
        {
            TempData["restaurantId"] = int.Parse(id);
            IEnumerable<Delivery> deliveries = _restaurantService.GetAllRestaurantDeliveries(int.Parse(id));
            return View("Deliveries", deliveries);
        }

        public ActionResult ApproveDelivery(string deliveryId)
        {
            int restaurantId = (int)TempData["restaurantId"];
            int id = int.Parse(deliveryId);
            Delivery delivery = _restaurantService.GetDeliveryById(int.Parse(deliveryId));
            delivery.DeliveryStatus = DeliveryStatus.InPreparation;
            _restaurantService.EditDelivery(delivery);
            IEnumerable<Delivery> deliveries = _restaurantService.GetAllRestaurantDeliveries(restaurantId);
            return View("Deliveries", deliveries);
        }

        public ActionResult EditRestaurantForm(string id)
        {
            int restaurantId = int.Parse(id);
            Restaurant restaurant = _restaurantService.GetRestaurantByFilter(x => x.Id == restaurantId);
            TempData["restaurantId"] = restaurant.Id;
            return View(restaurant);
        }

        public ActionResult EditRestaurant(string restaurantName, string restaurantStatus, string StreetName, string StreetNumber, string ApartmentNumber,
            string Floor, string City, string PostalCode, string Province, string Country, string RestaurantPhoneNumber, string RestaurantEmail, string RestaurantWebsite,
            string PreparationTime, string MinimumOrderPrice, string FreeDeliveryOrderPrice)
        {
            int restaurantId = (int)TempData["restaurantId"];
            Restaurant restaurantToEdit = _restaurantService.GetRestaurantByFilter(x => x.Id == restaurantId);
            restaurantToEdit.RestaurantName = restaurantName;
            restaurantToEdit.RestaurantAddress = new Address()
            {
                StreetName = StreetName,
                ApartmentNumber = int.Parse(ApartmentNumber),
                City = City,
                Country = Country,
                Floor = int.Parse(Floor),
                PostalCode = PostalCode,
                Province = Province,
                StreetNumber = int.Parse(StreetNumber)
            };

            restaurantToEdit.RestaurantPhoneNumber = RestaurantPhoneNumber;
            restaurantToEdit.RestaurantEmail = RestaurantEmail;
            restaurantToEdit.RestaurantWebsite = RestaurantWebsite;
            restaurantToEdit.FreeDeliveryOrderPrice = float.Parse(FreeDeliveryOrderPrice);
            restaurantToEdit.MinimumOrderPrice = float.Parse(MinimumOrderPrice);
            restaurantToEdit.PreparationTime = int.Parse(PreparationTime);

            if (restaurantStatus == "Closed")
            {
                restaurantToEdit.RestaurantStatus = RestaurantStatus.Closed;
            }
            else if (restaurantStatus == "Open")
            {
                restaurantToEdit.RestaurantStatus = RestaurantStatus.Open;
            }
            else
            {
                restaurantToEdit.RestaurantStatus = RestaurantStatus.Unavailable;
            }

            restaurantToEdit.BusinessHours = (ICollection<BusinessHours>)_restaurantService.GetBusinessHoursByFilter(x => x.RestaurantId == restaurantToEdit.Id);
            //restaurantToEdit.UserId = User.Identity.GetUserId();
            _restaurantService.EditRestaurantInfo(restaurantToEdit);
            return RedirectToAction("ListRestaurants");

        }
    }
}
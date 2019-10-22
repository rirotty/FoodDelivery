using System;
using System.Collections.Generic;
using System.Linq;
using FoodDeliveryProject.Model.Models;
using FoodDeliveryProject.DataAccess.UnitOfWork;
using System.Linq.Expressions;
using FoodDeliveryProject.Model.Enumerations;
using Microsoft.AspNet.Identity;

namespace FoodDeliveryProject.Services.RestaurantService
{
    public class RestaurantService : IRestaurantService
    {
        private IUnitOfWork _unitOfWork;
        public RestaurantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddRestaurant(RestaurantViewModel restaurantVM, string currentUserId)
        {
            Restaurant restaurant = new Restaurant()
            {
                RestaurantName = restaurantVM.RestaurantName,
                RestaurantPhoneNumber = restaurantVM.RestaurantPhoneNumber,
                RestaurantEmail = restaurantVM.RestaurantEmail,
                Foods = new List<Food>()
            };
            Address restaurantAddress = new Address()
            {
                StreetName = restaurantVM.RestaurantAddress.StreetName,
                StreetNumber = restaurantVM.RestaurantAddress.StreetNumber,
                ApartmentNumber = restaurantVM.RestaurantAddress.ApartmentNumber,
                City = restaurantVM.RestaurantAddress.City,
                Country = restaurantVM.RestaurantAddress.Country,
                PostalCode = restaurantVM.RestaurantAddress.PostalCode,
                Province = restaurantVM.RestaurantAddress.Province,
                Floor = restaurantVM.RestaurantAddress.Floor
            };
            restaurant.RestaurantAddress = restaurantAddress;
            if (restaurantVM.RestaurantWebsite != null)
            {
                restaurant.RestaurantWebsite = restaurantVM.RestaurantWebsite;
            }
            if (restaurantVM.PreparationTime != null)
            {
                restaurant.PreparationTime = int.Parse(restaurantVM.PreparationTime);
            }
            if (restaurantVM.MinimumOrderPrice != null)
            {
                restaurant.MinimumOrderPrice = float.Parse(restaurantVM.MinimumOrderPrice);
            }
            if (restaurantVM.FreeDeliveryOrderPrice != null)
            {
                restaurant.FreeDeliveryOrderPrice = float.Parse(restaurantVM.FreeDeliveryOrderPrice);
            }

            restaurant.UserId = currentUserId;
            restaurant.RestaurantRegistrationStatus = RestaurantRegistrationStatus.PendingForApproval;
            restaurant.VacationDays = new List<VacationDays>();
            DateTime vacationFrom;
            DateTime vacationTo;
            if (DateTime.TryParse(restaurantVM.VacationFrom, out vacationFrom) && DateTime.TryParse(restaurantVM.VacationTo, out vacationTo))
            {
                restaurant.VacationDays.Add(new VacationDays() { From = vacationFrom, To = vacationTo });
            }
            _unitOfWork.RestaurantRepository.Add(restaurant);
            _unitOfWork.Commit();
        }

        public void AddBusinessHours(Restaurant restaurant, string mondayOpeningHours, string tuesdayOpeningHours, string wednesdayOpeningHours, string thursdayOpeningHours, string fridayOpeningHours,
            string saturdayOpeningHours, string sundayOpeningHours, string mondayClosingHours, string tuesdayClosingHours, string wednesdayClosingHours, string thursdayClosingHours, string fridayClosingHours,
            string saturdayClosingHours, string sundayClosingHours)
        {
            restaurant.BusinessHours = new List<BusinessHours>(7);
            restaurant.BusinessHours.Add(new BusinessHours() { Day = DaysOfTheWeek.Monday, OpeningHours = DateTime.Parse(mondayOpeningHours), ClosingHours = DateTime.Parse(mondayClosingHours) });
            restaurant.BusinessHours.Add(new BusinessHours() { Day = DaysOfTheWeek.Tuesday, OpeningHours = DateTime.Parse(tuesdayOpeningHours), ClosingHours = DateTime.Parse(tuesdayClosingHours) });
            restaurant.BusinessHours.Add(new BusinessHours() { Day = DaysOfTheWeek.Wednesday, OpeningHours = DateTime.Parse(wednesdayOpeningHours), ClosingHours = DateTime.Parse(wednesdayClosingHours) });
            restaurant.BusinessHours.Add(new BusinessHours() { Day = DaysOfTheWeek.Thursday, OpeningHours = DateTime.Parse(thursdayOpeningHours), ClosingHours = DateTime.Parse(thursdayClosingHours) });
            restaurant.BusinessHours.Add(new BusinessHours() { Day = DaysOfTheWeek.Friday, OpeningHours = DateTime.Parse(fridayOpeningHours), ClosingHours = DateTime.Parse(fridayClosingHours) });
            restaurant.BusinessHours.Add(new BusinessHours() { Day = DaysOfTheWeek.Saturday, OpeningHours = DateTime.Parse(saturdayOpeningHours), ClosingHours = DateTime.Parse(saturdayClosingHours) });
            restaurant.BusinessHours.Add(new BusinessHours() { Day = DaysOfTheWeek.Sunday, OpeningHours = DateTime.Parse(sundayOpeningHours), ClosingHours = DateTime.Parse(sundayClosingHours) });

            EditRestaurantInfo(restaurant);
        }

        public bool AddFoodToRestaurant(string[] addToAll, string currentUserId, Food foodToAdd, string restaurantName)
        {
            if (addToAll != null)
            {
                foreach (var r in GetRestaurantsByOwnerId(currentUserId))
                {
                    if (r.Foods != null)
                    {
                        foreach (var n in r.Foods)
                        {
                            if (n.FoodName == foodToAdd.FoodName)
                            {
                                return false;
                            }
                        }
                    }

                    Food newFood = new Food() { FoodDescription = foodToAdd.FoodDescription, FoodName = foodToAdd.FoodName, FoodPhoto = foodToAdd.FoodPhoto, FoodPrice = foodToAdd.FoodPrice, FoodStatus = foodToAdd.FoodStatus, FoodType = foodToAdd.FoodType, Subcategory = foodToAdd.Subcategory, Ingredients = foodToAdd.Ingredients };
                    newFood.Restaurant = r;
                    AddFood(newFood.FoodName, newFood.FoodDescription, newFood.FoodPrice.ToString(), newFood.FoodStatus.ToString(), newFood.FoodType.ToString(), newFood.SubcategoryId.ToString());
                }
            }
            else if (restaurantName != null)
            {
                Restaurant restaurant = GetRestaurantByFilter(x => x.RestaurantName == restaurantName && x.UserId == currentUserId);
                if (restaurant.Foods != null)
                {
                    foreach (var f in restaurant.Foods)
                    {
                        if (f.FoodName == foodToAdd.FoodName)
                        {
                            return false;
                        }
                    }
                }
                foodToAdd.Restaurant = restaurant;
                EditFood(foodToAdd, foodToAdd.FoodName, foodToAdd.FoodDescription, foodToAdd.FoodPrice.ToString(), foodToAdd.FoodStatus.ToString(), foodToAdd.FoodType.ToString());
            }
            return true;
        }
        public void AddCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.Commit();
        }
        public void AddSubcategory(Subcategory subcategory)
        {
            _unitOfWork.SubcategoryRepository.Add(subcategory);
            _unitOfWork.Commit();
        }
        public Food AddFood(string name, string description, string price, string status, string type, string subcategoryId)
        {
            Food food = new Food() { FoodName = name, FoodPrice = float.Parse(price), FoodDescription = description };
            food.Subcategory = GetSubcategoryById(int.Parse(subcategoryId));
            if (status == "available")
            {
                food.FoodStatus = FoodStatus.Available;
            }
            else
            {
                food.FoodStatus = FoodStatus.Unavailable;
            }
            if (type == "standard")
            {
                food.FoodType = FoodType.Standard;
            }
            else
            {
                food.FoodType = FoodType.Custom;
                food.Ingredients = new List<Ingredient>();
            }
            _unitOfWork.FoodRepository.Add(food);
            _unitOfWork.Commit();
            return food;
        }
        public void AddIngredient(Food food, string name, string price, string description)
        {
            Ingredient ingredient = new Ingredient() { IngredientName = name, IngredientPrice = float.Parse(price) };
            if (description != null)
            {
                ingredient.IngredientDescription = description;
            }
            ingredient.Food = food;

            _unitOfWork.IngredientRepository.Add(ingredient);
            _unitOfWork.Commit();
        }
        public void AddDiscount(Discount discount)
        {
            _unitOfWork.DiscountRepository.Add(discount);
            _unitOfWork.Commit();
        }
        public void EditCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Edit(category);
            _unitOfWork.Commit();
        }
        public void EditSubcategory(Subcategory subcategory)
        {
            _unitOfWork.SubcategoryRepository.Edit(subcategory);
            _unitOfWork.Commit();
        }
        public void EditFood(Food foodToEdit, string name, string description, string price, string status, string type)
        {
            foodToEdit.FoodName = name;
            foodToEdit.FoodDescription = description;
            foodToEdit.FoodPrice = float.Parse(price);
            if (status == "available")
            {
                foodToEdit.FoodStatus = FoodStatus.Available;
            }
            else
            {
                foodToEdit.FoodStatus = FoodStatus.Unavailable;
            }
            if (type == "custom")
            {
                foodToEdit.FoodType = FoodType.Custom;
            }
            else
            {
                foodToEdit.FoodType = FoodType.Standard;
                foodToEdit.Ingredients = new List<Ingredient>();
            }
            //edit subcategory
            _unitOfWork.FoodRepository.Edit(foodToEdit);
            _unitOfWork.Commit();
        }
        public void EditIngredient(Ingredient ingredientToEdit, string name, string price, string description)
        {
            ingredientToEdit.IngredientName = name;
            ingredientToEdit.IngredientPrice = float.Parse(price);
            if (description != null)
                ingredientToEdit.IngredientDescription = description;
            _unitOfWork.IngredientRepository.Edit(ingredientToEdit);
            _unitOfWork.Commit();
        }

        public Food RemoveIngredient(string ingredientId)
        {
            int id = int.Parse(ingredientId);
            Ingredient ingredientToRemove = GetIngredientsByFilter(x => x.Id == id).FirstOrDefault();
            int foodId = (int)ingredientToRemove.FoodId;
            ingredientToRemove.FoodId = null;
            _unitOfWork.IngredientRepository.Edit(ingredientToRemove);

            Food food = GetFoodByFilter(x => x.Id == foodId);
            food.Ingredients = GetIngredientsByFilter(x => x.FoodId == food.Id && x.Food.RestaurantId == food.RestaurantId);
            return food;
        }
        public void EditDiscount(Discount discount)
        {
            _unitOfWork.DiscountRepository.Edit(discount);
            _unitOfWork.Commit();
        }
        public void EditDelivery(Delivery delivery)
        {
            _unitOfWork.DeliveryRepository.Edit(delivery);
            _unitOfWork.Commit();
        }
        public void ChangeDeliveryStatus(Delivery delivery)
        {
            _unitOfWork.DeliveryRepository.Edit(delivery);
            _unitOfWork.Commit();
        }
        public void EditRestaurantInfo(Restaurant restaurant)
        {
            _unitOfWork.RestaurantRepository.Edit(restaurant);
            _unitOfWork.Commit();
        }
        public ApplicationUser GetCurrentUserById(string id)
        {
            return _unitOfWork.UserRepository.UserManager.FindById(id);
        }
        public Category GetCategoryById(int id)
        {
            return _unitOfWork.CategoryRepository.GetById(id);
        }
        public Subcategory GetSubcategoryById(int id)
        {
            return _unitOfWork.SubcategoryRepository.GetById(id);
        }
        public Food GetFoodByFilter(Expression<Func<Food, bool>> filter)
        {
            return _unitOfWork.FoodRepository.Get(filter).FirstOrDefault();
        }
        public Delivery GetDeliveryById(int id)
        {
            return _unitOfWork.DeliveryRepository.GetById(id);
        }
        public Restaurant GetRestaurantByFilter(Expression<Func<Restaurant, bool>> filter)
        {
            return _unitOfWork.RestaurantRepository.Get(filter).FirstOrDefault();
        }
        public IEnumerable<Restaurant> GetRestaurantsByOwnerId(string ownerId)
        {
            return _unitOfWork.RestaurantRepository.Get(x => x.UserId == ownerId);
        }
        public IEnumerable<Ingredient> GetIngredientsByFilter(Expression<Func<Ingredient, bool>> filter)
        {
            return _unitOfWork.IngredientRepository.Get(filter);
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _unitOfWork.CategoryRepository.ReadAll();
        }
        public IEnumerable<Subcategory> GetSubcategoriesByCategoryId(int categoryId)
        {
            return _unitOfWork.SubcategoryRepository.Get(x => x.CategoryId == categoryId);
        }
        public IEnumerable<Food> GetFoodBySubcategoryId(int subcategoryId)
        {
            return _unitOfWork.FoodRepository.Get(x => x.SubcategoryId == subcategoryId).GroupBy(f => f.FoodName).Select(group => group.First());
        }
        public IEnumerable<Delivery> GetAllRestaurantDeliveries(int restaurantId)
        {
            var deliveries = from delivery in _unitOfWork.DeliveryRepository.ReadAll()
                             join bills in _unitOfWork.BillRepository.ReadAll() on delivery.Id equals bills.DeliveryId
                             join order in _unitOfWork.OrderRepository.ReadAll() on bills.Id equals order.BillId
                             where order.Food.RestaurantId == restaurantId
                             select delivery;

            return deliveries;
        }
        public IEnumerable<Delivery> GetRestaurantDeliveriesByFilter(Expression<Func<Delivery, bool>> filter, int restaurantId)
        {
            IEnumerable<Delivery> filteredDeliveries = _unitOfWork.DeliveryRepository.Get(filter);
            var returnDeliveries = from delivery in filteredDeliveries
                                   join bills in _unitOfWork.BillRepository.ReadAll() on delivery.Id equals bills.DeliveryId
                                   join order in _unitOfWork.OrderRepository.ReadAll() on bills.Id equals order.BillId
                                   where order.Food.RestaurantId == restaurantId
                                   select delivery;

            return returnDeliveries;
        }
        public IEnumerable<Food> GetAllRestaurantFood(int restaurantId)
        {
            return _unitOfWork.FoodRepository.Get(x => x.RestaurantId == restaurantId);
        }
        public IEnumerable<Food> GetRestaurantFoodByFilter(Expression<Func<Food, bool>> filter, int restaurantId)
        {
            IEnumerable<Food> filteredFood = _unitOfWork.FoodRepository.Get(filter);
            var returnFood = from food in filteredFood
                             join restaurant in _unitOfWork.RestaurantRepository.ReadAll() on food.RestaurantId equals restaurant.Id
                             where food.RestaurantId == restaurantId
                             select food;
            return returnFood;
        }
        public IEnumerable<Discount> GetAllRestaurantDiscounts(int restaurantId)
        {
            var discounts = from discount in _unitOfWork.DiscountRepository.ReadAll()
                            join food in _unitOfWork.FoodRepository.ReadAll() on discount.FoodId equals food.Id
                            where food.RestaurantId == restaurantId
                            select discount;
            return discounts;
        }
        public IEnumerable<Discount> GetCurrentRestaurantDiscounts(int restaurantId)
        {
            var discounts = from discount in _unitOfWork.DiscountRepository.ReadAll()
                            join food in _unitOfWork.FoodRepository.ReadAll() on discount.FoodId equals food.Id
                            where food.RestaurantId == restaurantId && DateTime.Now >= discount.DiscountStartDate && DateTime.Now <= discount.DiscountEndDate
                            select discount;
            return discounts;
        }
        public IEnumerable<Discount> GetRestaurantDiscountsByFilter(Expression<Func<Discount, bool>> filter, int restaurantId)
        {
            IEnumerable<Discount> filteredDiscounts = _unitOfWork.DiscountRepository.Get(filter);
            var returnDiscounts = from discount in filteredDiscounts
                                  join food in _unitOfWork.FoodRepository.ReadAll() on discount.FoodId equals food.Id
                                  where food.RestaurantId == restaurantId
                                  select discount;
            return returnDiscounts;
        }

        public IEnumerable<BusinessHours> GetBusinessHoursByFilter(Expression<Func<BusinessHours, bool>> filter)
        {
            return _unitOfWork.BusinessHoursRepository.Get(filter);
        }
    }
}

using FoodDeliveryProject.Model.Enumerations;
using FoodDeliveryProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FoodDeliveryProject.Services.RestaurantService
{
    public interface IRestaurantService
    {
        void AddRestaurant(RestaurantViewModel restaurantVM, string currentUserId);
        void AddBusinessHours(Restaurant restaurant, string mondayOpeningHours, string tuesdayOpeningHours, string wednesdayOpeningHours, string thursdayOpeningHours, string fridayOpeningHours,
            string saturdayOpeningHours, string sundayOpeningHours, string mondayClosingHours, string tuesdayClosingHours, string wednesdayClosingHours, string thursdayClosingHours, string fridayClosingHours,
            string saturdayClosingHours, string sundayClosingHours);
        bool AddFoodToRestaurant(string[] addToAll, string currentUserId, Food foodToAdd, string restaurantName);
        void AddCategory(Category category);
        void AddSubcategory(Subcategory subcategory);
        Food AddFood(string name, string description, string price, string status, string type, string subcategoryId);
        void AddIngredient(Food food, string name, string price, string description);
        void AddDiscount(Discount discount);
        void EditCategory(Category category);
        void EditSubcategory(Subcategory subcategory);
        void EditFood(Food foodToEdit, string name, string description, string price, string status, string type);
        void EditDelivery(Delivery delivery);
        void EditDiscount(Discount discount);
        void EditIngredient(Ingredient ingredientToEdit, string name, string price, string description);
        Food RemoveIngredient(string ingredientId);
        void EditRestaurantInfo(Restaurant restaurant);
        void ChangeDeliveryStatus(Delivery delivery);
        ApplicationUser GetCurrentUserById(string id);
        Category GetCategoryById(int id);
        Subcategory GetSubcategoryById(int id);
        Delivery GetDeliveryById(int id);
        Food GetFoodByFilter(Expression<Func<Food, bool>> filter);
        Restaurant GetRestaurantByFilter(Expression<Func<Restaurant, bool>> filter);
        IEnumerable<Ingredient> GetIngredientsByFilter(Expression<Func<Ingredient, bool>> filter);
        IEnumerable<Restaurant> GetRestaurantsByOwnerId(string ownerId);
        IEnumerable<Category> GetAllCategories();
        IEnumerable<Subcategory> GetSubcategoriesByCategoryId(int categoryId);
        IEnumerable<Delivery> GetAllRestaurantDeliveries(int restaurantId);
        IEnumerable<Delivery> GetRestaurantDeliveriesByFilter(Expression<Func<Delivery, bool>> filter, int restaurantId);
        IEnumerable<Food> GetFoodBySubcategoryId(int subcategoryId);
        IEnumerable<Food> GetAllRestaurantFood(int restaurantId);
        IEnumerable<Food> GetRestaurantFoodByFilter(Expression<Func<Food, bool>> filter, int restaurantId);
        IEnumerable<Discount> GetAllRestaurantDiscounts(int restaurantId);
        IEnumerable<Discount> GetCurrentRestaurantDiscounts(int restaurantId);
        IEnumerable<Discount> GetRestaurantDiscountsByFilter(Expression<Func<Discount, bool>> filter, int restaurantId);
        IEnumerable<BusinessHours> GetBusinessHoursByFilter(Expression<Func<BusinessHours, bool>> filter);
    }
}

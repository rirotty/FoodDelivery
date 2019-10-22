using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDeliveryProject.Model.Models;
using FoodDeliveryProject.DataAccess.UnitOfWork;

namespace FoodDeliveryProject.Services.RestaurantService
{
    public class RestaurantService : IRestaurantService
    {
        private IUnitOfWork _unitOfWork;
        public RestaurantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.Commit();
        }

        public void AddFood(Food food)
        {  
            _unitOfWork.FoodRepository.Add(food);
            _unitOfWork.Commit();
        }

        public void AddSubcategory(Subcategory subcategory)
        {
            _unitOfWork.SubcategoryRepository.Add(subcategory);
            _unitOfWork.Commit();
        }

        public void ChangeDeliveryStatus(Delivery delivery)
        {
            _unitOfWork.DeliveryRepository.Edit(delivery);
            _unitOfWork.Commit();
        }

        public void EditCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Edit(category);
            _unitOfWork.Commit();
        }

        public void EditFood(Food food)
        {
            _unitOfWork.FoodRepository.Edit(food);
            _unitOfWork.Commit();
        }

        public void EditRestaurantInfo(Restaurant restaurant)
        {
            _unitOfWork.RestaurantRepository.Edit(restaurant);
            _unitOfWork.Commit();
        }

        public void EditSubcategory(Subcategory subcategory)
        {
            _unitOfWork.SubcategoryRepository.Edit(subcategory);
            _unitOfWork.Commit();
        }

        public IEnumerable<Delivery> GetRestaurantDeliveries(int restaurantId)
        {
            var query = (from delivery in _unitOfWork.DeliveryRepository.ReadAll()
                        join order in _unitOfWork.OrderRepository.ReadAll() on delivery.BillId equals order.BillId
                        where order.Food.RestaurantId == restaurantId
                        select delivery);
            return query;             
        }

        public IEnumerable<Food> GetRestaurantFood(int restaurantId)
        {
            return _unitOfWork.FoodRepository.Get(x => x.RestaurantId == restaurantId);
        }
    }
}

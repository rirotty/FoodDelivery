using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using System.Data.Entity;
using FoodDeliveryProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(DbContext dbContext) : base(dbContext){ }

        public FoodDeliveryDbContext DatabaseContext
        {
            get { return _context as FoodDeliveryDbContext; }
        }

        public int GetIdByName(string restaurantName)
        {
            int id = (from restaurants in DatabaseContext.RestaurantSet
                      where restaurants.RestaurantName == restaurantName
                      select new { restaurants.Id }).FirstOrDefault().Id;

            return id;
        }

        public IEnumerable<Restaurant> GetRestaurantsInCity(string city)
        {
            return Get(x => x.RestaurantAddress.City == city).ToList();
        }
    }
}

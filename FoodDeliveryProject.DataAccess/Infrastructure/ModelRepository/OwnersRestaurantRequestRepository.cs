using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    public class OwnersRestaurantRequestRepository : Repository<OwnersRestaurantRequest>, IOwnersRestaurantRequestRepository
    {
        private FoodDeliveryDbContext _dbContext;

        public OwnersRestaurantRequestRepository(FoodDeliveryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public OwnersRestaurantRequest FindByUserId(string userId)
        {
            return (from restaurantRequest in _dbContext.RestaurantRequestsSet
                    join user in _dbContext.Users on restaurantRequest.UserId equals user.Id
                    join restaurant in _dbContext.RestaurantSet on restaurantRequest.RestaurantId equals restaurant.Id
                    where restaurantRequest.UserId == userId
                    select restaurantRequest).FirstOrDefault();
        }

        public int FindIdByUserId(string userId)
        {
            return (from restaurantRequest in _dbContext.RestaurantRequestsSet
                    where restaurantRequest.UserId == userId
                    select restaurantRequest.Id).FirstOrDefault();
        }

        public OwnersRestaurantRequest FindPopulatedById(int id)
        {
            return (from restaurantRequest in _dbContext.RestaurantRequestsSet
                    join user in _dbContext.Users on restaurantRequest.UserId equals user.Id
                    join restaurant in _dbContext.RestaurantSet on restaurantRequest.RestaurantId equals restaurant.Id
                    where restaurantRequest.Id == id
                    select restaurantRequest).FirstOrDefault();
        }

        public IEnumerable<OwnersRestaurantRequest> GetRequests()
        {
            var requests = (from restaurantRequest in _dbContext.RestaurantRequestsSet
                            join user in _dbContext.Users on restaurantRequest.UserId equals user.Id
                            join restaurant in _dbContext.RestaurantSet on restaurantRequest.RestaurantId equals restaurant.Id
                            select new { restaurantRequest.Id, user, restaurant });

            List<OwnersRestaurantRequest> temp = new List<OwnersRestaurantRequest>();
            foreach (var restaurantRequest in requests)
            {
                OwnersRestaurantRequest request = new OwnersRestaurantRequest()
                {
                    Id = restaurantRequest.Id,
                    User = restaurantRequest.user,
                    Restaurant = restaurantRequest.restaurant
                };
                temp.Add(request);
            }
            return temp;
        }

        public IEnumerable<OwnersRestaurantRequest> GetPendingRequests()
        {
            var requests = (from restaurantRequest in _dbContext.RestaurantRequestsSet
                            join user in _dbContext.Users on restaurantRequest.UserId equals user.Id
                            join restaurant in _dbContext.RestaurantSet on restaurantRequest.RestaurantId equals restaurant.Id
                            where restaurantRequest.RequestStatus == Model.Enumerations.UserRequestStatus.PendingForApproval
                            select new { restaurantRequest.Id, user, restaurant });

            List<OwnersRestaurantRequest> temp = new List<OwnersRestaurantRequest>();
            foreach (var restaurantRequest in requests)
            {
                OwnersRestaurantRequest request = new OwnersRestaurantRequest()
                {
                    Id = restaurantRequest.Id,
                    User = restaurantRequest.user,
                    Restaurant = restaurantRequest.restaurant
                };
                temp.Add(request);
            }
            return temp;
        }
    }
}

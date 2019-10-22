using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    public class UsersRoleRequestRepository : Repository<UsersRoleRequest>, IUsersRoleRequestRepository
    {
        private FoodDeliveryDbContext _dbContext;

        public UsersRoleRequestRepository(FoodDeliveryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public UsersRoleRequest FindPopulatedById(int id)
        {
            return (from roleRequest in _dbContext.RoleRequestsSet
                    join user in _dbContext.Users on roleRequest.UserId equals user.Id
                    join role in _dbContext.Roles on roleRequest.RoleId equals role.Id
                    where roleRequest.Id == id
                    select roleRequest).FirstOrDefault();
        }

        public UsersRoleRequest FindByUserId(string userId)
        {
            return (from roleRequest in _dbContext.RoleRequestsSet
                    join user in _dbContext.Users on roleRequest.UserId equals user.Id
                    join role in _dbContext.Roles on roleRequest.RoleId equals role.Id
                    where roleRequest.UserId == userId
                    select roleRequest).FirstOrDefault();
        }

        public int FindIdByUserId(string userId)
        {
            return (from roleRequest in _dbContext.RoleRequestsSet
                    where roleRequest.UserId == userId
                    select roleRequest.Id).FirstOrDefault();
        }

        public IEnumerable<UsersRoleRequest> GetRequests()
        {
            var requests = (from roleRequest in _dbContext.RoleRequestsSet
                            join user in _dbContext.Users on roleRequest.UserId equals user.Id
                            join role in _dbContext.Roles on roleRequest.RoleId equals role.Id
                            select new { roleRequest.Id, user, role, roleRequest.RequestStatus, roleRequest.ValidationDocumentPath});

            List<UsersRoleRequest> temp = new List<UsersRoleRequest>();
            foreach (var roleRequest in requests)
            {
                UsersRoleRequest request = new UsersRoleRequest()
                {
                    Id = roleRequest.Id,
                    User = roleRequest.user,
                    Role = roleRequest.role,
                    RequestStatus = roleRequest.RequestStatus,
                    ValidationDocumentPath = roleRequest.ValidationDocumentPath
                };
                temp.Add(request);
            }
            return temp;
        }

        public IEnumerable<UsersRoleRequest> GetRequests(string selectedRole)
        {
            var requests = (from roleRequest in _dbContext.RoleRequestsSet
                            join user in _dbContext.Users on roleRequest.UserId equals user.Id
                            join role in _dbContext.Roles on roleRequest.RoleId equals role.Id
                            where role.Name == selectedRole
                            select new { roleRequest.Id, user, role, roleRequest.RequestStatus, roleRequest.ValidationDocumentPath });

            List<UsersRoleRequest> temp = new List<UsersRoleRequest>();
            foreach (var roleRequest in requests)
            {
                UsersRoleRequest request = new UsersRoleRequest()
                {
                    Id = roleRequest.Id,
                    User = roleRequest.user,
                    Role = roleRequest.role,
                    RequestStatus = roleRequest.RequestStatus,
                    ValidationDocumentPath = roleRequest.ValidationDocumentPath
                };
                temp.Add(request);
            }
            return temp;
        }

        public IEnumerable<UsersRoleRequest> GetPendingRequests()
        {
            var requests = (from roleRequest in _dbContext.RoleRequestsSet
                            join user in _dbContext.Users on roleRequest.UserId equals user.Id
                            join role in _dbContext.Roles on roleRequest.RoleId equals role.Id
                            where roleRequest.RequestStatus == Model.Enumerations.UserRequestStatus.PendingForApproval
                            select new { roleRequest.Id, user, role, roleRequest.RequestStatus, roleRequest.ValidationDocumentPath });

            List<UsersRoleRequest> temp = new List<UsersRoleRequest>();
            foreach (var roleRequest in requests)
            {
                UsersRoleRequest request = new UsersRoleRequest()
                {
                    Id = roleRequest.Id,
                    User = roleRequest.user,
                    Role = roleRequest.role,
                    RequestStatus = roleRequest.RequestStatus,
                    ValidationDocumentPath = roleRequest.ValidationDocumentPath
                };
                temp.Add(request);
            }
            return temp;
        }
    }
}

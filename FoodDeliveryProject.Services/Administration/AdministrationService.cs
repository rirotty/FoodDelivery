using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDeliveryProject.DataAccess.UnitOfWork;
using FoodDeliveryProject.Model.Models;
using FoodDeliveryProject.Services.Administration.UsersRequestsInterfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FoodDeliveryProject.Services.Administration
{
    public class AdministrationService : IAdministrationService
    {
        private IUnitOfWork _unitOfWork;

        public AdministrationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddRole(string roleName)
        {
            await _unitOfWork.UserRepository.RoleManager.CreateAsync(new IdentityRole() { Name = roleName });
            _unitOfWork.Commit();
        }

        public IEnumerable<string> GetAllRoles()
        {
            var identityRoles = _unitOfWork.UserRepository.RoleManager.Roles;
            List<string> roles = new List<string>();
            foreach (IdentityRole role in identityRoles)
            {
                roles.Add(role.Name);
            }
            return roles;
        }

        public async Task<IEnumerable<string>> GetAllNonUserRoles(string userName)
        {
            string userId = (await _unitOfWork.UserRepository.UserManager.FindByNameAsync(userName)).Id;
            var userRoles = await _unitOfWork.UserRepository.UserManager.GetRolesAsync(userId);

            var identityRoles = _unitOfWork.UserRepository.RoleManager.Roles;
            List<string> roles = new List<string>();
            foreach (IdentityRole role in identityRoles)
            {
                if (!userRoles.Contains(role.Name))
                {
                    roles.Add(role.Name);
                }
            }
            return roles;
        }

        public async Task RemoveRole(string roleName)
        {
            IdentityRole role = await _unitOfWork.UserRepository.RoleManager.FindByNameAsync(roleName);
            await _unitOfWork.UserRepository.RoleManager.DeleteAsync(role);
            _unitOfWork.Commit();
        }

        public IEnumerable<UsersRoleRequest> ListAllRoleRequests()
        {
            return _unitOfWork.RoleRequestRepository.GetRequests();
        }

        public async Task GenerateRoleRequest(string username, string role, string documentationPath)
        {
            _unitOfWork.RoleRequestRepository.Add(new UsersRoleRequest()
            {
                UserId = (await _unitOfWork.UserRepository.UserManager.FindByNameAsync(username)).Id,
                RoleId = (await _unitOfWork.UserRepository.RoleManager.FindByNameAsync(role)).Id,
                RequestStatus = Model.Enumerations.UserRequestStatus.PendingForApproval,
                ValidationDocumentPath = documentationPath
            });
            _unitOfWork.Commit();
        }

        public void RemoveRoleRequest(int id)
        {
            var request = _unitOfWork.RoleRequestRepository.GetById(id);
            _unitOfWork.RoleRequestRepository.Delete(request);
            _unitOfWork.Commit();
        }

        public async Task ApproveRoleRequest(int id)
        {
            var request = _unitOfWork.RoleRequestRepository.GetById(id);
            request.RequestStatus = Model.Enumerations.UserRequestStatus.Accepted;
            _unitOfWork.RoleRequestRepository.Edit(request);
            await _unitOfWork.UserRepository.UserManager.AddToRoleAsync(request.UserId, request.Role.Name);
            _unitOfWork.Commit();
        }

        public void DeclineRoleRequest(int id)
        {
            var request = _unitOfWork.RoleRequestRepository.GetById(id);
            request.RequestStatus = Model.Enumerations.UserRequestStatus.Declined;
            _unitOfWork.RoleRequestRepository.Edit(request);
            _unitOfWork.Commit();
        }

        public IEnumerable<OwnersRestaurantRequest> ListAllRequestsForRestaurants()
        {
            return _unitOfWork.RestaurantRequestRepository.GetRequests();
        }

        public void ApproveRestaurantRequest(int id)
        {
            var request = _unitOfWork.RestaurantRequestRepository.GetById(id);
            request.RequestStatus = Model.Enumerations.UserRequestStatus.Accepted;
            _unitOfWork.RestaurantRequestRepository.Edit(request);
            var restaurant = _unitOfWork.RestaurantRepository.GetById(request.RestaurantId);
            restaurant.UserId = request.UserId;
            _unitOfWork.RestaurantRepository.Edit(restaurant);
            _unitOfWork.Commit();
        }

        public async Task GenerateRestaurantRequest(string userName, string restaurantName, string documentationPath)
        {
            _unitOfWork.RestaurantRequestRepository.Add(new OwnersRestaurantRequest()
            {
                UserId = (await _unitOfWork.UserRepository.UserManager.FindByNameAsync(userName)).Id,
                RestaurantId = _unitOfWork.RestaurantRepository.GetIdByName(restaurantName),
                RequestStatus = Model.Enumerations.UserRequestStatus.PendingForApproval,
                ValidationDocumentPath = documentationPath
            });
            _unitOfWork.Commit();
        }

        public void RemoveRestaurantRequest(int id)
        {
            var request = _unitOfWork.RestaurantRequestRepository.GetById(id);
            _unitOfWork.RestaurantRequestRepository.Delete(request);
            _unitOfWork.Commit();
        }

        public void DeclineRestaurantRequest(int id)
        {
            var request = _unitOfWork.RestaurantRequestRepository.GetById(id);
            request.RequestStatus = Model.Enumerations.UserRequestStatus.Declined;
            _unitOfWork.RestaurantRequestRepository.Edit(request);
            _unitOfWork.Commit();
        }

    }
}

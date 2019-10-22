using System.Collections.Generic;
using System.Linq;
using FoodDeliveryProject.Model.Models;
using FoodDeliveryProject.DataAccess.UnitOfWork;
using FoodDeliveryProject.Model.Enumerations;
using Microsoft.AspNet.Identity;

namespace FoodDeliveryProject.Services.DeliveryBoyService
{
    public class DeliveryBoyService : IDeliveryBoyService
    {
        private IUnitOfWork _unitOfWork;
        public DeliveryBoyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        ApplicationUser GetCurrentUserById(string id)
        {
            return _unitOfWork.UserRepository.UserManager.FindById(id);
        }

        public IEnumerable<Delivery> GetMyDeliveries(string deliveryBoyId)
        {
            var query = (from delivery in _unitOfWork.DeliveryRepository.ReadAll()
                         join deliveryBoy in _unitOfWork.UserRepository.ReadAll() on delivery.DeliveryBoyId equals deliveryBoy.Id
                         where delivery.DeliveryBoyId == deliveryBoyId
                         select delivery);
            return query;
        }

        public IEnumerable<Delivery> GetAvailableDeliveries()
        {
            return _unitOfWork.DeliveryRepository.Get(x => x.DeliveryStatus == DeliveryStatus.InPreparation);
        }

        public Delivery GetCurrentDelivery(string deliveryBoyId)
        {
            return _unitOfWork.DeliveryRepository.Get(x => x.DeliveryStatus == DeliveryStatus.Shipping && x.DeliveryBoyId == deliveryBoyId).FirstOrDefault();
        }

        //public Delivery GetCurrentDelivery(IEnumerable<Delivery> allMyDeliveries)
        //{
        //    var query = (from allMyDeliveries in _unitOfWork.DeliveryRepository.ReadAll()
        //                 join deliveryBoy in _unitOfWork.UserRepository.ReadAll() on delivery.UserId equals deliveryBoy.Id
        //                 where delivery.UserId == deliveryBoyId
        //                 select delivery);
        //    return query
        //}
        //public void CurrentDeliveryDirections(Delivery CurrentDelivery)
        //{

        //}
        //public bool ConfirmDelivery()
        //{

        //}
        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _unitOfWork.UserRepository.UserManager.Users;
        }

        
    }
}

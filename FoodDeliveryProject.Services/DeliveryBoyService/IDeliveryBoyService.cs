using FoodDeliveryProject.Model.Models;
using System.Collections.Generic;

namespace FoodDeliveryProject.Services.DeliveryBoyService
{
    public interface IDeliveryBoyService
    {
        IEnumerable<Delivery> GetMyDeliveries(string deliveryBoyId);
        IEnumerable<Delivery> GetAvailableDeliveries();
        Delivery GetCurrentDelivery(string deliveryBoyId);
        //Delivery GetNextDelivery();
        //void CurrentDeliveryDirections(Delivery CurrentDelivery);
        //bool ConfirmDelivery();
        IEnumerable<ApplicationUser> GetAllUsers();
        
    }
}

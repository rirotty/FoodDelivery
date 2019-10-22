using FoodDeliveryProject.Model.Models;
using FoodDeliveryProject.Services.DeliveryBoyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryProject.Web.Controllers
{
    public class DeliveryBoyController : Controller
    {
        
        private IDeliveryBoyService _deliveryBoyService;
        public DeliveryBoyController(IDeliveryBoyService deliveryBoyService)
        {
            _deliveryBoyService = deliveryBoyService;
        }

        // GET: DeliveryBoy
        public ActionResult Index()
        {

            //Delivery first =_deliveryService.GetAvailableDeliveries().FirstOrDefault();

            Delivery first;
            if(_deliveryBoyService.GetAvailableDeliveries().First() == null)
            {
                first = new Delivery();
            }

            first = _deliveryBoyService.GetAvailableDeliveries().First();
            return View(first);
        }
    }
}
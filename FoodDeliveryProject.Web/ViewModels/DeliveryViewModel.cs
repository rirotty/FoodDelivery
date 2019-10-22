using FoodDeliveryProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryProject.Web.ViewModels
{
    public class DeliveryViewModel
    {
        public string MyAddress { get; set; }
        public AddressViewModel Address { get; set; }
        public string PaymentMethod { get; set; }
    }
}
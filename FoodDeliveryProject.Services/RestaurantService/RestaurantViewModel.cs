using FoodDeliveryProject.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDeliveryProject.Services.RestaurantService
{
    public class RestaurantViewModel
    {
        [Required]
        public string RestaurantName { get; set; }
        [Required]
        public Address RestaurantAddress { get; set; }
        [Required]
        public string RestaurantPhoneNumber { get; set; }
        [Required]
        public string RestaurantEmail { get; set; }
        public string RestaurantWebsite { get; set; }
        public string RestaurantPhoto { get; set; }
        public string PreparationTime { get; set; }
        public string MinimumOrderPrice { get; set; }
        public string FreeDeliveryOrderPrice { get; set; }
        public string VacationFrom { get; set; }
        public string VacationTo { get; set; }
        public string RestaurantStatus { get; set; }
    }
}
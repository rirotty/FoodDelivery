using FoodDeliveryProject.Model.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryProject.Model.Models
{
    public class Restaurant : EntityBase
    {
        [Required]
        public string RestaurantName { get; set; }
        public int? RestaurantAddressId { get; set; }
        [Required]
        [ForeignKey("RestaurantAddressId")]
        public virtual Address RestaurantAddress { get; set; }
        [Required]
        public string RestaurantPhoneNumber { get; set; }
        [Required]
        public string RestaurantEmail { get; set; }
        public string RestaurantWebsite { get; set; }
        public string RestaurantPhoto { get; set; }
        public int PreparationTime { get; set; }
        public float MinimumOrderPrice { get; set; }
        public float FreeDeliveryOrderPrice { get; set; }
        public RestaurantStatus RestaurantStatus { get; set; }
        public RestaurantRegistrationStatus RestaurantRegistrationStatus { get; set; }
        public virtual string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser RestaurantOwner { get; set; }
        public ICollection<Food> Foods { get; set; }
        public ICollection<BusinessHours> BusinessHours { get; set; }
        public ICollection<VacationDays> VacationDays { get; set; }
    }
}

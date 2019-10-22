using FoodDeliveryProject.Model.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryProject.Model.Models
{
    public class BusinessHours : EntityBase
    {
        public virtual int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }
        [Required]
        public DaysOfTheWeek Day { get; set; }
        [Required]
        public DateTime OpeningHours { get; set; }
        [Required]
        public DateTime ClosingHours { get; set; }
    }
}

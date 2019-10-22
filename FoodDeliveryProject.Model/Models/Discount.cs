using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryProject.Model.Models
{
    public class Discount : EntityBase
    {
        public string DiscountName { get; set; }
        public string DiscountDescription { get; set; }
        public int FoodId { get; set; }
        [ForeignKey("FoodId")]
        public virtual Food Food { get; set; }
        [Required]
        public DateTime DiscountStartDate { get; set; }
        [Required]
        public DateTime DiscountEndDate { get; set; }
        [Required]
        public int Percentage { get; set; }
    }
}

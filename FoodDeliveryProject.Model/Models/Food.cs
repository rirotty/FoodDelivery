using FoodDeliveryProject.Model.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryProject.Model.Models
{
    public class Food : EntityBase
    {
        [Required(ErrorMessage = "This field is required")]
        public string FoodName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public float FoodPrice { get; set; }
        public string FoodDescription { get; set; }
        public string FoodPhoto { get; set; }
        public FoodStatus FoodStatus { get; set; }
        public FoodType FoodType { get; set; }
        public int SubcategoryId { get; set; }
        [ForeignKey("SubcategoryId")]
        public virtual Subcategory Subcategory { get; set; }
        public int? RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}

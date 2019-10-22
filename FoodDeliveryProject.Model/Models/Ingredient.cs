using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryProject.Model.Models
{
    public class Ingredient : EntityBase
    {
        [Required]
        public string IngredientName { get; set; }
        public string IngredientDescription { get; set; }
        [Required]
        public float IngredientPrice { get; set; }
        public int? FoodId { get; set; }
        [ForeignKey("FoodId")]
        public virtual Food Food { get; set; }
    }
}

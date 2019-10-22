using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryProject.Model.Models
{
    public class Subcategory : EntityBase
    {
        [Required]
        public string SubcategoryName { get; set; }
        public string SubcategoryDescription { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public ICollection<Food> Foods { get; set; }
    }
}

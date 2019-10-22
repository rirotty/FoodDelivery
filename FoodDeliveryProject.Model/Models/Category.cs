using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryProject.Model.Models
{
    public class Category : EntityBase
    {
        [Required]
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; }
    }
}

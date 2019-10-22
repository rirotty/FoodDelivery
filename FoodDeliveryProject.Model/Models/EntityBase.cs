using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryProject.Model.Models
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}

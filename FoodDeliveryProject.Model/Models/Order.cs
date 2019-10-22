using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryProject.Model.Models
{
    public class Order : EntityBase
    {
        [ForeignKey("FoodId")]
        public virtual Food Food { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("BillId")]
        public virtual  Bill Bill { get; set; }
        public int BillId { get; set; }
    }
}

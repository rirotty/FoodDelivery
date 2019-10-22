using FoodDeliveryProject.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryProject.Model.Models
{
    public class Bill : EntityBase
    {
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }
        public DateTime BillDate { get; set; }
        public float TotalPrice { get; set; }
        public BillStatus BillStatus { get; set; }
        public int ? DeliveryId { get; set; }
        [ForeignKey("DeliveryId")]
        public virtual Delivery Delivery { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
    }
}

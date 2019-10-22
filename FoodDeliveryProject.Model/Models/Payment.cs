using FoodDeliveryProject.Model.Enumerations;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryProject.Model.Models
{
    public class Payment : EntityBase
    {
        public string PaymentMethod { get; set; }
        public PaymentStatus Status { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime PaymentDate { get; set; }
        public virtual Collection<Delivery> Deliveries { get; set; }
    }
}

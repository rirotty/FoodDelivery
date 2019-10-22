using FoodDeliveryProject.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryProject.Model.Models
{   
    public class Delivery : EntityBase
    {
        public string DeliveryBoyId { get; set; }
        [ForeignKey("DeliveryBoyId")]
        public virtual ApplicationUser DeliveryBoy { get; set; }
        public virtual int SpecifiedAddressId { get; set; }
        [ForeignKey("SpecifiedAddressId")]
        public virtual Address SpecifiedAddress { get; set; }
        public virtual List<Bill> Bills { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DeliveryDeparture { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DeliveryArrival { get; set; }
        public int PaymentId { get; set; }
        [ForeignKey("PaymentId")]
        public virtual Payment Payment { get; set; }
    }
}

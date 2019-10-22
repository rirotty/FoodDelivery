using FoodDeliveryProject.Model.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryProject.Model.Models
{
    public class OwnersRestaurantRequest : EntityBase
    {
        public virtual string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public virtual int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }
        public UserRequestStatus RequestStatus { get; set; }
        public string ValidationDocumentPath { get; set; }
        [Required]
        public DateTime TimeOfRequest { get; set; }
    }
}

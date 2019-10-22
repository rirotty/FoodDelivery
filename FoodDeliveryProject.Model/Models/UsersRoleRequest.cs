using FoodDeliveryProject.Model.Enumerations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryProject.Model.Models
{
    public class UsersRoleRequest : EntityBase
    {
        public virtual string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public virtual string RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual IdentityRole Role { get; set; }
        public UserRequestStatus RequestStatus { get; set; }
        public string ValidationDocumentPath { get; set; }
        [Required]
        public DateTime TimeOfRequest { get; set; }
    }
}

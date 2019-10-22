using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using FoodDeliveryProject.Model.Enumerations;
using System;

namespace FoodDeliveryProject.Model.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        public ICollection<Bill> Bills { get; set; }
        public ICollection<Restaurant> Restaurants { get; set; }
        public ICollection<Delivery> Deliveries { get; set; }
        public DeliveryBoyStatus DeliveryBoyStatus { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

       
    }
}
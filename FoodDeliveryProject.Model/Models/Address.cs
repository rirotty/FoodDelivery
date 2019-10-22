using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryProject.Model.Models
{
    public class Address : EntityBase
    {
        [Required]
        public string StreetName { get; set; }
        [Required]
        public int StreetNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [MinLength(4), MaxLength(15)]
        public string PostalCode { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Country { get; set; }
        public int ApartmentNumber { get; set; }
        public int Floor { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}

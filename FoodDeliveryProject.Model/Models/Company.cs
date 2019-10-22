using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryProject.Model.Models
{
    public class Company : EntityBase
    {
        public string Name { get; set; }
        public int CompanyAddressId { get; set; }
        [ForeignKey("CompanyAddressId")]
        public virtual Address CompanyAddress { get; set; }
        public List<ApplicationUser> Employees { get; set; }
        public List<Bill> Bills { get; set; }
    }
}
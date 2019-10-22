using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDeliveryProject.Web.ViewModels
{
    public class AddressViewModel
    {
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string Floor { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
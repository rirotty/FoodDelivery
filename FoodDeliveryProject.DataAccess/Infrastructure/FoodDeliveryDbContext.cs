using FoodDeliveryProject.Model.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FoodDeliveryProject.DataAccess.Infrastructure
{
    public class FoodDeliveryDbContext : IdentityDbContext<ApplicationUser>
    {
        public FoodDeliveryDbContext() : base("DefaultConnection") { }

        public static FoodDeliveryDbContext Create()
        {
            return new FoodDeliveryDbContext();
        }

        public DbSet<Address> AddressSet { get; set; }
        public DbSet<Bill> BillSet { get; set; }
        public DbSet<BusinessHours> BuisnessHoursSet { get; set; }
        public DbSet<Company> CompanySet { get; set; }
        public DbSet<Category> CategorySet { get; set; }
        public DbSet<Delivery> DeliverySet { get; set; }
        public DbSet<Discount> DiscountSet { get; set; }
        public DbSet<Food> FoodSet { get; set; }
        public DbSet<Ingredient> IgredientSet { get; set; }
        public DbSet<Order> OrderSet { get; set; }
        public DbSet<Payment> PaymentSet { get; set; }
        public DbSet<Subcategory> SubcategorySet { get; set; }
        public DbSet<VacationDays> VacationDaysSet { get; set; }
        public DbSet<Restaurant> RestaurantSet { get; set; }
        public DbSet<OwnersRestaurantRequest> RestaurantRequestsSet { get; set; }
        public DbSet<UsersRoleRequest> RoleRequestsSet { get; set; }

    }
}

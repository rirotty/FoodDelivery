using FoodDeliveryProject.DataAccess.Infrastructure;
using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository;
using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository.ApplicationUserManagement;
using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.DataAccess.UnitOfWork;
using FoodDeliveryProject.Model.Models;
using FoodDeliveryProject.Services.Administration;
using FoodDeliveryProject.Services.CustomerService;
using FoodDeliveryProject.Services.RestaurantService;
using FoodDeliveryProject.Services.UserValidation;
using FoodDeliveryProject.Services.UserValidation.ManageUsersService;
using FoodDeliveryProject.Web.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Web;
using Unity;
using Unity.Injection;

namespace FoodDeliveryProject.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ApplicationSignInManager>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            
            container.RegisterFactory<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication);
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new InjectionConstructor(typeof(FoodDeliveryDbContext)));
            container.RegisterType<IRoleStore<IdentityRole, string>, RoleStore<IdentityRole>>(new InjectionConstructor(typeof(FoodDeliveryDbContext)));
            container.RegisterType<IApplicationUserRegisterService, ApplicationUserRegisterService>();
            container.RegisterType<IApplicationUserSignInService, ApplicationUserSignInService>();
            container.RegisterType<IManageUsersServise, ManageUsersService>();
            container.RegisterType<AuthorizeHandler>();
            container.RegisterType<IAdministrationService, AdministrationService>();
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<IRestaurantService, RestaurantService>();

            container.RegisterType<AccountController>();
            container.RegisterType<ManageController>();
        }
    }
}
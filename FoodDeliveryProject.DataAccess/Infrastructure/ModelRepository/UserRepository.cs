using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository.ApplicationUserManagement;
using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationRoleManager _roleManager;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private UserStore<ApplicationUser> _store;
        private IDataProtectionProvider _dataProtectionProvider;
        private IAuthenticationManager _authenticationManager;
        private IRoleStore<IdentityRole, string> _roleStore;

        public UserRepository(UserStore<ApplicationUser> store, IDataProtectionProvider dataProtectionProvider, IAuthenticationManager authenticationManager, IRoleStore<IdentityRole, string> roleStore)
        {
            _roleStore = roleStore;
            _store = store;
            _dataProtectionProvider = dataProtectionProvider;
            _authenticationManager = authenticationManager;
            UserDbContext = _store.Context;
        }

        public ApplicationUser GetUserById(string userId)
        {
            return UserManager.FindById(userId);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    _userManager = new ApplicationUserManager(_store, _dataProtectionProvider);
                }
                return _userManager;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                if (_signInManager == null)
                {
                    _signInManager = new ApplicationSignInManager(UserManager, _authenticationManager);
                }
                return _signInManager;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (_roleManager == null)
                {
                    _roleManager = new ApplicationRoleManager(_roleStore);
                }
                return _roleManager;
            }
        }

        public DbContext UserDbContext { get; private set; }

        public IEnumerable<ApplicationUser> ReadAll()
        {
            return UserManager.Users.ToList();
        }

        public Address GetUsersAddress(string id)
        {
            Address addressQuery = (from users in _store.Context.Set<ApplicationUser>()
                                    where users.Id == id
                                    select users.Address).FirstOrDefault();

            return addressQuery;
        }
    }
}
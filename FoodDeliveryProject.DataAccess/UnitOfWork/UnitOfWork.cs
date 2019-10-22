using FoodDeliveryProject.DataAccess.Infrastructure;
using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepository;
using Microsoft.AspNet.Identity;
using FoodDeliveryProject.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace FoodDeliveryProject.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAddressRepository _addressRepository;
        private IBillRepository _billRepository;
        private IBusinessHoursRepository _businessHoursRepository;
        private ICategoryRepository _cateogoryRepository;
        private IDeliveryRepository _deliveryRepository;
        private IDiscountRepository _discountRepository;
        private IFoodRepository _foodRepository;
        private IIngredientRepository _ingredientRepository;
        private IOrderRepository _orderRepository;
        private IPaymentRepository _paymentRepository;
        private IRestaurantRepository _restaurantRepository;
        private ISubcategoryRepository _subcategoryRepository;
        private IVacationDaysRepository _vacationDaysRepository;
        private IUserRepository _userRepository;
        private IUsersRoleRequestRepository _roleRequestRepository;
        private IOwnersRestaurantRequestRepository _restaurantRequestRepository;

        private readonly FoodDeliveryDbContext _context;

        // Note that a user of UnitOfWork will pass a context and it will be used in all of the repositories
        public UnitOfWork(FoodDeliveryDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public virtual IAddressRepository AddressRepository
        {
            get
            {
                if (_addressRepository == null)
                {
                    _addressRepository = new AddressRepository(_context);
                }
                return _addressRepository;
            }
        }

        public virtual IUserRepository UserRepository
        {
            get
            {
                return _userRepository;
            }
        }

        public IBillRepository BillRepository
        {
            get
            {
                if (_billRepository == null)
                {
                    _billRepository = new BillRepository(_context);
                }
                return _billRepository;
            }
        }

        public IBusinessHoursRepository BusinessHoursRepository
        {
            get
            {
                if (_businessHoursRepository == null)
                {
                    _businessHoursRepository = new BusinessHoursRepository(_context);
                }
                return _businessHoursRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_cateogoryRepository == null)
                {
                    _cateogoryRepository = new CategoryRepository(_context);
                }
                return _cateogoryRepository;
            }
        }

        public IDeliveryRepository DeliveryRepository
        {
            get
            {
                if (_deliveryRepository == null)
               {
                    _deliveryRepository = new DeliveryRepository(_context);
                }
                return _deliveryRepository;
            }
        }

        public IDiscountRepository DiscountRepository
        {
            get
            {
                if (_discountRepository == null)
                {
                    _discountRepository = new DiscountRepository(_context);
                }
                return _discountRepository;
            }
        }

        public IFoodRepository FoodRepository
        {
            get
            {
                if (_foodRepository == null)
                {
                    _foodRepository = new FoodRepository(_context);
                }
                return _foodRepository;
            }
        }

        public IIngredientRepository IngredientRepository
        {
            get
            {
                if (_ingredientRepository == null)
                {
                    _ingredientRepository = new IngredientRepository(_context);
                }
                return _ingredientRepository;
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new OrderRepository(_context);
                }
                return _orderRepository;
            }
        }

        public IPaymentRepository PaymentRepository
        {
            get
            {
                if (_paymentRepository == null)
                {
                    _paymentRepository = new PaymentRepository(_context);
                }
                return _paymentRepository;
            }
        }

        public IRestaurantRepository RestaurantRepository
        {
            get
            {
                if (_restaurantRepository == null)
                {
                    _restaurantRepository = new RestaurantRepository(_context);
                }
                return _restaurantRepository;
            }
        }

        public ISubcategoryRepository SubcategoryRepository
        {
            get
            {
                if (_subcategoryRepository == null)
                {
                    _subcategoryRepository = new SubcategoryRepository(_context);
                }
                return _subcategoryRepository;
            }
        }

        public IVacationDaysRepository VacationDaysRepository
        {
            get
            {
                if (_vacationDaysRepository == null)
                {
                    _vacationDaysRepository = new VacationDaysRepository(_context);
                }
                return _vacationDaysRepository;
            }
        }

        public IUsersRoleRequestRepository RoleRequestRepository
        {
            get
            {
                if (_roleRequestRepository == null)
                {
                    _roleRequestRepository = new UsersRoleRequestRepository(_context);
                }
                return _roleRequestRepository;
            }
        }

        public IOwnersRestaurantRequestRepository RestaurantRequestRepository
        {
            get
            {
                if (_restaurantRequestRepository == null)
                {
                    _restaurantRequestRepository = new OwnersRestaurantRequestRepository(_context);
                }
                return _restaurantRequestRepository;
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
            //UserRepository.DbContext.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

using FoodDeliveryProject.DataAccess.Infrastructure.ModelRepositoryInterfaces;
using System;

namespace FoodDeliveryProject.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        IAddressRepository AddressRepository { get; }
        IBillRepository BillRepository { get; }
        IBusinessHoursRepository BusinessHoursRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IDeliveryRepository DeliveryRepository { get; }
        IDiscountRepository DiscountRepository { get; }
        IFoodRepository FoodRepository { get; }
        IIngredientRepository IngredientRepository { get; }
        IOrderRepository OrderRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IRestaurantRepository RestaurantRepository { get; }
        ISubcategoryRepository SubcategoryRepository { get; }
        IVacationDaysRepository VacationDaysRepository { get; }
        IUserRepository UserRepository { get; }
        IUsersRoleRequestRepository RoleRequestRepository { get; }
        IOwnersRestaurantRequestRepository RestaurantRequestRepository { get; }
    }

}

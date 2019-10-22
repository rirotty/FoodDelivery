using FoodDeliveryProject.DataAccess.UnitOfWork;
using System.Threading.Tasks;

namespace FoodDeliveryProject.Services.UserValidation
{
    public class AuthorizeHandler
    {
        private IUnitOfWork _unitOfWork;

        public AuthorizeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> IsInRole(string id, string selectedRole)
        {
            var userRoles = await _unitOfWork.UserRepository.UserManager.GetRolesAsync(id);

            if (userRoles.Contains(selectedRole))
            {
                return true;
            }
            return false;
        }
    }
}

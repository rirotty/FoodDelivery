using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryProject.Web.CustomAttributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private string _selectedRole;
        public CustomAuthorizeAttribute(string selectedRole)
        {
            _selectedRole = selectedRole;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authenticated = base.AuthorizeCore(httpContext);
            if (!authenticated)
            {
                return false;
            }

            if (httpContext.User.IsInRole(_selectedRole))
            {
                return true;
            }

            return false;
        }
    }
}

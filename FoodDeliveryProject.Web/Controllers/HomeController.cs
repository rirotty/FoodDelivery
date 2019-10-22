using FoodDeliveryProject.Services.UserValidation;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FoodDeliveryProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private AuthorizeHandler _authorizeHandler;

        public HomeController(AuthorizeHandler handler)
        {
            _authorizeHandler = handler;
        }

        public async Task<ActionResult> Index()
        {
            if (User.Identity.Name == String.Empty)
            {
                return View();
            }

            if (await _authorizeHandler.IsInRole(User.Identity.GetUserId(), "Admin"))
            {
                return RedirectToAction("Index", "Administration");
            }
            else if (await _authorizeHandler.IsInRole(User.Identity.GetUserId(), "Owner"))
            {
                return RedirectToAction("Index", "Restaurant");
            }
            else if (await _authorizeHandler.IsInRole(User.Identity.GetUserId(), "Customer"))
            {
                return RedirectToAction("Index", "Customer", null);
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
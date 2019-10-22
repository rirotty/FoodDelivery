using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using FoodDeliveryProject.Web.ViewModels;
using Microsoft.Owin.Security;
using System.Web;
using FoodDeliveryProject.Services.UserValidation.ManageUsersService;
using FoodDeliveryProject.Services.UserValidation.Enums;
using FoodDeliveryProject.Services.Administration;
using System.IO;
using System;

namespace FoodDeliveryProject.Web.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private IManageUsersServise _manageUsersService;
        private IAuthenticationManager _authenticationManager;
        private IAdministrationService _administrationService;

        public ManageController()
        {
        }

        public ManageController(IManageUsersServise manageUsersService, IAuthenticationManager authenticationManager, IAdministrationService administrationService)
        {
            _manageUsersService = manageUsersService;
            _authenticationManager = authenticationManager;
            _administrationService = administrationService;
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = _manageUsersService.HasPassword(userId),
                PhoneNumber = await _manageUsersService.GetPhoneNumber(userId),
                TwoFactor = await _manageUsersService.HasTwoFactorEnabled(userId),
                Logins = await _manageUsersService.GetInternalLogins(userId),
                BrowserRemembered = await _authenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        // GET: /Manage/ApplyForPartnership
        [HttpGet]
        public async Task<ActionResult> ApplyForPartnership()
        {
            ViewBag.Name = new SelectList(await _administrationService.GetAllNonUserRoles(User.Identity.Name));
            return View();
        }
        
        // POST: /Manage/ApplyForPartnership
        [HttpPost]
        public ActionResult ApplyForPartnership(UsersRoleRequestViewModel model)
        {
            //TODO: Save uploaded file to file system and generate request
            if (ModelState.IsValid)
            {
                if (model.ValidationDocument.ContentLength > 0)
                {
                    string filePath = Path.Combine(HttpContext.Server.MapPath("/App_Data/Uploads"), String.Concat(Guid.NewGuid().ToString(), Path.GetExtension(model.ValidationDocument.FileName)));
                    model.ValidationDocument.SaveAs(filePath);
                    _administrationService.GenerateRoleRequest(User.Identity.GetUserName(), model.Role, filePath);
                }
            }

            return RedirectToAction("Index");
        } 

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            message = await _manageUsersService.RemoveLogin(User, new UserLoginInfo(loginProvider, providerKey));
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _manageUsersService.AddPhoneNumber(User.Identity.GetUserId(), model.Number);
            
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await _manageUsersService.EnableTwoFactorAuthentification(User.Identity.GetUserId());
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await _manageUsersService.DisableTwoFactorAuthentification(User.Identity.GetUserId());
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        //public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        //{
        //    var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
        //    // Send an SMS through the SMS provider to verify the phone number
        //    return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        //}

        //
        // POST: /Manage/VerifyPhoneNumber
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
        //    if (result.Succeeded)
        //    {
        //        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //        if (user != null)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //        }
        //        return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
        //    }
        //    // If we got this far, something failed, redisplay form
        //    ModelState.AddModelError("", "Failed to verify phone");
        //    return View(model);
        //}

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            ManageMessageId message = await _manageUsersService.RemovePhoneNumber(User.Identity.GetUserId());
            return RedirectToAction("Index", new { Message = message });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ManageMessageId message = await _manageUsersService.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            return RedirectToAction("Index", new { Message = message });
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ManageMessageId message = await _manageUsersService.SetPassword(User.Identity.GetUserId(), model.NewPassword);
            return RedirectToAction("Index", new { Message = message });
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var userLogins = await _manageUsersService.GetInternalLogins(User.Identity.GetUserId());
            //var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                //OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        //public async Task<ActionResult> LinkLoginCallback()
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        //    }
        //    var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
        //    return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        //}

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
#endregion
    }
}
using FoodDeliveryProject.Services.Administration;
using FoodDeliveryProject.Web.CustomAttributes;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FoodDeliveryProject.Web.Controllers
{
    [CustomAuthorize("Admin")]
    public class AdministrationController : Controller
    {
        private IAdministrationService _administrationService;

        public AdministrationController(IAdministrationService service)
        {
            _administrationService = service;
        }

        // GET: Administration
        public ActionResult Index()
        {
            var roleRequests = _administrationService.ListAllRoleRequests();

            return View(roleRequests);
        }

        // GET: Administration/ManageRoles
        public ActionResult ManageRoles()
        {
            var roles = _administrationService.GetAllRoles();

            return View(roles);
        }

        // POST: Administration/AddRole
        public async Task<ActionResult> AddRole(string role)
        {
            await _administrationService.AddRole(role);

            return RedirectToAction("ManageRoles");
        }

        // DELETE: Administration/RemoveRole
        public ActionResult RemoveRole(string role)
        {
            _administrationService.RemoveRole(role);
            return RedirectToAction("ManageRoles");
        }



        // PUT: Administration/ApproveRoleRequest
        public async Task<ActionResult> ApproveRoleRequest(int requestId)
        {
            await _administrationService.ApproveRoleRequest(requestId);

            return RedirectToAction("Index");
        }
    }
}
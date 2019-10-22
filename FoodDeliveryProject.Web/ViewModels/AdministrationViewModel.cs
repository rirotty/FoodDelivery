using FoodDeliveryProject.Web.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace FoodDeliveryProject.Web.ViewModels
{
    public class UsersRoleRequestViewModel
    {
        [Required]
        public string Role { get; set; }
        [Required, FilesValidation(".txt,.pdf,.csv,.docx", ErrorMessage = "Specify a CSV, TXT, PDF, or DOCX file.")]
        public HttpPostedFileBase ValidationDocument { get; set; }
    }
}
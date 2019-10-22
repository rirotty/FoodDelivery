using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDeliveryProject.Web.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FilesValidationAttribute : ValidationAttribute
    {
        private List<string> AllowedExtensions { get; set; }

        public FilesValidationAttribute(string fileExtensions)
        {
            AllowedExtensions = fileExtensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public override bool IsValid(object value)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;

            if (file != null)
            {
                var fileName = file.FileName;

                return AllowedExtensions.Any(y => fileName.EndsWith(y));
            }

            return true;
        }
    }
}
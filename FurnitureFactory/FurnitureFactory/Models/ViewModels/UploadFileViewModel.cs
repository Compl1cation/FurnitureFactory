using FurnitureFactory.Models.Validation;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace FurnitureFactory.Models.ViewModels
{
    public class UploadFileViewModel
    {
        [Required]
        [FileSize(42400)]
        [FileTypes("jpg,jpeg,png")]
       public HttpPostedFileBase File { get; set; }
    }
}
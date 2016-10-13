using FurnitureFactory.Models.ViewModels;
using System.Web;
using System.Web.Mvc;

namespace FurnitureFactory.Models.CustomBinders
{
    public class AvatarCustomBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(UploadFileViewModel))
            {
                HttpRequestBase request = controllerContext.HttpContext.Request;
                HttpPostedFileBase avatar = request.Files["avatar"] as HttpPostedFileBase;

                return new UploadFileViewModel { File = avatar };
            }
            else
            {
                return base.BindModel(controllerContext, bindingContext);
            }
        }
    }
    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Vertex.ProjeYonetim.Filters
{
    public class AjaxOnly : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request.IsAjaxRequest();
            //throw new NotImplementedException();
        }
    }
}
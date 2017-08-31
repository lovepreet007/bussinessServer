using Microsoft.AspNetCore.Http;


using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAppServer.CustomAttributes
{
    public class SessionAttribute : ActionFilterAttribute
    {
        //private readonly IHttpContextAccessor _contextAccessor;
       // private HttpContext _contextAccessor;
       // private ISession _sessionContext => _contextAccessor.HttpContext.Session;
        //public SessionAttribute(IHttpContextAccessor contextAccessor)
        //{
        //    _contextAccessor = contextAccessor.HttpContext;
        //}
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
       {
            const string sessionKey = "IsAuthenticated";
            //var s = Convert.ToBoolean(filterContext.HttpContext.Items[sessionKey]);
            //var ss = Convert.ToBoolean(filterContext.HttpContext.Session.GetString(sessionKey));
            var sss = HttpHelper.HttpContext.Session.GetObjectFromJson<string>("IsAuthenticated");
          
          //HttpHelper.HttpContext.Session
            // var ll = _sessionContext.GetString(sessionKey);
            if (!Convert.ToBoolean(filterContext.HttpContext.Session.GetString(sessionKey)))
            {
                string url = "https://localhost:4200";

                System.Uri uri = new System.Uri(url);

                //redire(uri);

                // Response.Redirect("http://localhost:4200");
            }



             
       
       
      
    }
    }
}

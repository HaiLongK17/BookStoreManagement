using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;

namespace BlogManagement.UI.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoDirectAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var canAccess = false;
            var referer = context.HttpContext.Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                var request = context.HttpContext.Request;
                var rUri = new UriBuilder(referer).Uri;
                if (request.Host.Host == rUri.Host && request.Host.Port == rUri.Port && request.Scheme == rUri.Scheme)
                {
                    canAccess = true;
                }
            }

            if (!canAccess)
            {
                var cUrl = string.Format("{0}://{1}{2}{3}",
                    context.HttpContext.Request.Scheme, context.HttpContext.Request.Host,
                    context.HttpContext.Request.Path, context.HttpContext.Request.QueryString);
                if (cUrl.ToLower().Contains("admin/category/add") || cUrl.ToLower().Contains("admin/category/edit") || cUrl.ToLower().Contains("admin/category/delete"))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Category", action = "Index", area = "Admin" }));
                }
                if (cUrl.ToLower().Contains("admin/author/add") || cUrl.ToLower().Contains("admin/author/edit") || cUrl.ToLower().Contains("admin/author/delete"))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Author", action = "Index", area = "Admin" }));
                }
                if (cUrl.ToLower().Contains("admin/publisher/add") || cUrl.ToLower().Contains("admin/publisher/edit") || cUrl.ToLower().Contains("admin/publisher/delete"))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Publisher", action = "Index", area = "Admin" }));
                }
                if (cUrl.ToLower().Contains("admin/book/create") || cUrl.ToLower().Contains("admin/book/edit") || cUrl.ToLower().Contains("admin/book/delete"))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Book", action = "Index", area = "Admin" }));
                }
                if (cUrl.ToLower().Contains("admin/user/add") || cUrl.ToLower().Contains("admin/user/edit") ||
                    cUrl.ToLower().Contains("admin/user/changepassword") || cUrl.ToLower().Contains("admin/user/lockorunlock"))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Index", area = "Admin" }));
                }
            }
        }
    }
}

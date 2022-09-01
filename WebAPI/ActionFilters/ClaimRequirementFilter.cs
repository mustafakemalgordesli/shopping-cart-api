using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.DTOs;

namespace WebAPI.ActionFilters
{
    public class ClaimRequirementFilter : Attribute, IActionFilter
    {
        public string[] Roles;
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var req = context.HttpContext.Request;
            var res = context.HttpContext.Response;
            
            var id = context.HttpContext.Items["Id"];
            var role =  context.HttpContext.Items["Role"];
            if(id == null || role == null)
            {
                context.Result = new UnauthorizedObjectResult(new ErrorDetailDTO
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "UnAuthorized"
                });
                return;
            }
            // Role Checking 
            if(Roles.FirstOrDefault(x => x == role) == null)
            {
                context.Result = new UnauthorizedObjectResult(new ErrorDetailDTO
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "UnAuthorized"
                });
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
        }
    }
}

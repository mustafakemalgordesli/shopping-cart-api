using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebAPI.DTOs;
using WebAPI.Exceptions;
using WebAPI.Utils;

namespace WebAPI.ActionFilters
{
    public class LoginFilter : Attribute, IActionFilter
    {
        IJwtUtils _jwtUtils;
        public LoginFilter(IJwtUtils jwtUtils)
        {
            _jwtUtils = jwtUtils;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var req = context.HttpContext.Request;
            var res = context.HttpContext.Response;
            string token = req.Headers.Authorization.FirstOrDefault()?.Split(' ').Last();
            Tuple<int,string>? values = _jwtUtils.ValidateToken(token);
            int? id = values?.Item1;
            string? role = values?.Item2;
            if (id == null)
            {
                context.Result = new UnauthorizedObjectResult(new ErrorDetailDTO
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "UnAuthorized"
                });
                return;
            }
            context.HttpContext.Items["Id"] = id;
            if(role != null) context.HttpContext.Items["Role"] = role;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
        }
    }
}

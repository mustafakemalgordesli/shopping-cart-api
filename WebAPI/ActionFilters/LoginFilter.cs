using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebAPI.DTOs;
using WebAPI.Exceptions;
using WebAPI.Utils;

namespace WebAPI.ActionFilters
{
    public class LoginFilter : IActionFilter
    {
        IJwtUtils _jwtUtils;
        ICipherUtils _cipherUtils;
        public LoginFilter(IJwtUtils jwtUtils, ICipherUtils cipherUtils)
        {
            _jwtUtils = jwtUtils;
            _cipherUtils = cipherUtils;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var req = context.HttpContext.Request;
            var res = context.HttpContext.Response;
            string token = req.Headers.Authorization.ToString()?.Split(" ").Last();

            if (token == null)
            {
                context.Result = new UnauthorizedObjectResult(new ErrorDetailDTO
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "UnAuthorized"
                });
                return;
            }
            var decryptToken = _cipherUtils.Decrypt(token);
            Tuple<int,string>? values = _jwtUtils.ValidateToken(decryptToken);
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

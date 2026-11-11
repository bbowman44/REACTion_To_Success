using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Twisted.API.Attributes
{
    /// <summary>
    /// Rejects the request with a 401 unless the "Twisted-Key" header matches the configured "ApiKey".
    /// Apply to a controller or an individual action with [ApiKey]. Adding here for clarity on what it does so I remember when I hover
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAuthorizationFilter
    {
        public const string HeaderName = "Twisted-Key";

        public void OnAuthorization(AuthorizationFilterContext ctx)
        {
            var config = ctx.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            string? expected = config["ApiKey"];
            string provided = ctx.HttpContext.Request.Headers[HeaderName].ToString();

            if (string.IsNullOrEmpty(expected) || !CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(provided), Encoding.UTF8.GetBytes(expected)))
            {
                ctx.Result = new UnauthorizedResult();   // stops the request with a 401 if the provided and expexted key do not match
            }
        }
    }
}

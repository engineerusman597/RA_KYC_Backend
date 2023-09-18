using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RA_KYC_BE.API.Extensions;

namespace RA_KYC_BE.API.Controllers
{
    /// <summary>
    /// Base api controller to be used across apis.
    /// </summary>
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// User Id.
        /// </summary>
        protected int UserId => User.Claims.GetUserId();

        /// <summary>
        /// Username
        /// </summary>
        protected string Username => User.Claims.GetUsername();

        /// <summary>
        /// User Role
        /// </summary>
        protected string UserRole => User.Claims.GetUserRole();

        /// <summary>
        /// Date Time
        /// </summary>
        protected DateTime Now => DateTime.Now;

        /// <summary>
        /// Base url.
        /// </summary>
        protected string BaseUrl => Request.Scheme + "://" + Request.Host + Request.PathBase;
    }
}

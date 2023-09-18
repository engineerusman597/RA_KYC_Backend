using System.Security.Claims;

namespace RA_KYC_BE.API.Extensions
{
    /// <summary>
    /// Claim extensions
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get User Id
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static int GetUserId(this IEnumerable<Claim> claims)
        {
            return Convert.ToInt32(claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        }

        /// <summary>
        /// Get username
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static string GetUsername(this IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }

        /// <summary>
        /// Get user role
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static string GetUserRole(this IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        }
    }
}

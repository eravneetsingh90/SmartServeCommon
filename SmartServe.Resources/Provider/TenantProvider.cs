using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartServe.Resources.Provider
{
    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal User =>
            _httpContextAccessor.HttpContext?.User
            ?? throw new UnauthorizedAccessException("No HttpContext available.");

        public int TenantId =>
            int.Parse(User.FindFirst("tenant_id")?.Value
                ?? throw new UnauthorizedAccessException("TenantId not found in token."));

        public int UserId =>
            int.Parse(User.FindFirst("user_id")?.Value
                ?? throw new UnauthorizedAccessException("UserId not found in token."));

        public string? Role =>
            User.FindFirst(ClaimTypes.Role)?.Value;

        public bool IsOffline => throw new NotImplementedException();
    }

}

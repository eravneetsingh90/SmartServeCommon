using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Models
{
    public class LoginResponse
    {
        public string Role { get; set; }

        public string AccessToken { get; set; } = default!;

        public DateTime ExpiresAt { get; set; }

        public string Name { get; set; } = default!;

    }
}

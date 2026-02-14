namespace SmartServe.Domain.Models
{
    public class CurrentUser
    {
        public int TenantId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; } = string.Empty;
        public bool IsOffline { get; set; }
    }
}

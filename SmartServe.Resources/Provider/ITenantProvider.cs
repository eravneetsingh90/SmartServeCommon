namespace SmartServe.Resources.Provider
{
    public interface ITenantProvider
    {
        int TenantId { get; }
        int UserId { get; }
        string? Role { get; }
        bool IsOffline { get; }
    }
}

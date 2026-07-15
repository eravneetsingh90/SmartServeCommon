using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Authorization
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(UserEntity user);
    }
}

using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public interface IAuthService
	{
		Task<UserEntity?> LoginAsync(string username, string pin);
	}
}

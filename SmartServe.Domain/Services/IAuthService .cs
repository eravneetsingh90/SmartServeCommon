using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public interface IAuthService
	{
		Task<User?> LoginAsync(string username, string pin);
	}
}

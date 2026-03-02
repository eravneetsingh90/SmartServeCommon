using SmartServe.Common.Models;
using SmartServe.Domain.Models;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public interface IAuthService
	{
		Task<BaseResponse<LoginResponse>> LoginAsync(string username, string pin);
	}
}

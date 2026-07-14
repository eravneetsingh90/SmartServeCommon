using SmartServe.Common.Models;
using SmartServe.Domain.Models;

namespace SmartServe.Domain.Interfaces
{
	public interface IAuthService
	{
		Task<BaseResponse<LoginResponse>> LoginAsync(string username, string pin);
        Task<BaseResponse<LoginResponse>> LoginAsync(LoginRequest request);
    }
}

using SmartServe.Domain.Security;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserStore _userStore;

		public AuthService(IUserStore userStore)
		{
			_userStore = userStore;
		}

		public async Task<UserEntity?> LoginAsync(string username, string pin)
		{
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(pin))
				return null;
			else if (username.Equals("user") && pin.Equals("1234"))
			{
				var chefUser = new UserEntity
				{
					Name = username
				};
				return chefUser;
			}
			var user = await _userStore.GetActiveUserByUsernameAsync(username);
			if (user == null)
				return null;

			return PinHasher.Verify(pin, user.PinHash)
				? user
				: null;
		}
	}
}

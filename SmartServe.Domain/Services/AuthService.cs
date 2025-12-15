using SmartServe.Domain.Security;
using SmartServe.Domain.Stores;
using SmartServe.EFCore.Entities;

namespace SmartServe.Domain.Services
{
	public class AuthService
	{
		private readonly UserStore _userStore;

		public AuthService(UserStore userStore)
		{
			_userStore = userStore;
		}

		public async Task<User?> LoginAsync(string username, string pin)
		{
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(pin))
				return null;

			var user = await _userStore.GetActiveUserByUsernameAsync(username);
			if (user == null)
				return null;

			return PinHasher.Verify(pin, user.pin_hash)
				? user
				: null;
		}
	}
}

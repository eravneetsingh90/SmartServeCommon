using System.Security.Cryptography;
using System.Text;

namespace SmartServe.Domain.Security
{
	public static class PinHasher
	{
		public static string Hash(string pin)
		{
			using var sha = SHA256.Create();
			var bytes = Encoding.UTF8.GetBytes(pin);
			var hash = sha.ComputeHash(bytes);
			return Convert.ToBase64String(hash);
		}

		public static bool Verify(string pin, string storedHash)
		{
			return Hash(pin) == storedHash;
		}
	}
}


using System.Text;
using System.Security.Cryptography;


namespace Pastebin.Application.Utils
{
    public class Hash
    {
        public static string GeneratedHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}

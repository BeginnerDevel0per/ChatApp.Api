
using ChatApp.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Service.Security
{

    public class PasswordHash : IGeneratePasswordHash
    {
        public string GeneratePasswordHash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Şifreyi byte dizisine çeviriyoruz.
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // SHA256 kullanarak hashliyoruz.
                byte[] passwordHash = sha256.ComputeHash(passwordBytes);

                return Convert.ToBase64String(passwordHash);
            }
        }
    }

}

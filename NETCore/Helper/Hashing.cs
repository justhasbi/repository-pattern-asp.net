using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Helper
{
    public class Hashing
    {
        public string password;
        public string correctHash;

        public Hashing(string password)
        {
            this.password = password;
        }
        
        public Hashing(string password, string correctHash)
        {
            this.password = password;
            this.correctHash = correctHash;
        }

        public static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace project
{
    class Hash
    {
        private static string hash;
        private static byte[] salt = new byte[64];


        /// <summary>
        /// Method for salting and hashing input.
        /// </summary>
        /// <param name="text">User input.</param>
        public static void SaltandHash(string text)
        {
            var RNGProv = new RNGCryptoServiceProvider();
            // Generates salt.
            RNGProv.GetNonZeroBytes(salt);

            // Hashes input.
            var keyMaker = new Rfc2898DeriveBytes(text, salt, 10000);
            hash = Convert.ToBase64String(keyMaker.GetBytes(256));
        }


        /// <summary>
        /// Salts and hashes new input with those of previous input, then compares both inputs to see if they match.
        /// </summary>
        /// <param name="text">New input being compared against old.</param>
        public static bool Compare(string text)
        {
            // Hashes new input.
            var keyMaker = new Rfc2898DeriveBytes(text, salt, 10000);
            // Returns comparison of new and old input.
            return Convert.ToBase64String(keyMaker.GetBytes(256)) == hash;
        }
    }
}

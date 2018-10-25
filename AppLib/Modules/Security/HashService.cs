using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Moq;
using Xunit;
using AppLib.Core;

namespace AppLib.Modules.Security {
    public class HashService : BaseService, IHashService {

        public string Hash (string input) {
            if (input.Length == 0) {
                throw new ServiceException ("Invalid Operation Exception, Hash input cannot be an empty string");
            }
            var salt = GenerateSalt (16);
            var bytes = KeyDerivation.Pbkdf2 (input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);
            return $"{ Convert.ToBase64String(salt) }:{ Convert.ToBase64String(bytes) }";
        }

        public bool CheckMatch (string input, string hash) {
            try {
                var parts = hash.Split (':');
                var salt = Convert.FromBase64String (parts[0]);
                var bytes = KeyDerivation.Pbkdf2 (input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);

                return parts[1].Equals (Convert.ToBase64String (bytes));
            } catch {
                return false;
            }
        }

        private byte[] GenerateSalt (int length) {
            var salt = new byte[length];

            using (var random = RandomNumberGenerator.Create ()) {
                random.GetBytes (salt);
            }

            return salt;
        }
    }
}
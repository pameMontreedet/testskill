using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using AppLib.Core;
using Xunit;

namespace AppLib.Modules.Security {
    public class RandomService : BaseService, IRandomService {

        private const string AllowableCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";

        public string CreateRandomString (int length) {
            if(length <= 0){
                return "";
            }

            var bytes = new byte[length];

            using (var random = RandomNumberGenerator.Create ()) {
                random.GetBytes (bytes);
            }

            return new string (bytes.Select (x => AllowableCharacters[x % AllowableCharacters.Length]).ToArray ());
        }

        public string CreateUniqueRandomString () {
            return Guid.NewGuid ().ToString ().Replace ("-", "");
        }
    }
}
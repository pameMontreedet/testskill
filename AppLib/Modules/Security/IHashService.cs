using System;

namespace AppLib.Modules.Security {
    public interface IHashService {
        string Hash(string input);
        bool CheckMatch (string input, string hash);
    }
}
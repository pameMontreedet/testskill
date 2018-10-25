using System;

namespace AppLib.Modules.Security {
    public interface IRandomService {
        string CreateRandomString(int length);
        string CreateUniqueRandomString();
    }
}
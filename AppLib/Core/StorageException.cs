using System;
using AppLib.Core;

namespace AppLib.Core {
    public class StorageException : BaseException {
        public StorageException () { }

        public StorageException (string message) : base (message) { }

        public StorageException (string message, Exception inner) : base (message, inner) { }
    }
}
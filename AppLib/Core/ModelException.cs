using System;
using AppLib.Core;

namespace AppLib.Core {
    public class ModelException : BaseException {
        public ModelException () { }

        public ModelException (string message) : base (message) { }

        public ModelException (string message, Exception inner) : base (message, inner) { }
    }
}
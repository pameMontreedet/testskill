using System;
using AppLib.Core;

namespace AppLib.Core {
    public class ServiceException : BaseException {
        public ServiceException () { }

        public ServiceException (string message) : base (message) { }

        public ServiceException (string message, Exception inner) : base (message, inner) { }
    }
}
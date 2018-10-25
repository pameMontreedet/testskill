using System;
using Microsoft.Extensions.DependencyInjection;

namespace AppLib.Core {
    public abstract class BaseModule : IModule {
        public abstract void RegisterServices(IServiceCollection services);
    }
}
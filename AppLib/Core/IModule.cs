using System;
using Microsoft.Extensions.DependencyInjection;

namespace AppLib.Core {
    public interface IModule
    {
        void RegisterServices(IServiceCollection services);
    }
}
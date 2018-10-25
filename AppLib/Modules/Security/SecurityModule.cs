using System;
using AppLib.Core;
using Microsoft.Extensions.DependencyInjection;

namespace AppLib.Modules.Security {
    public class SecurityModule : BaseModule {
        public override void RegisterServices (IServiceCollection services) {
            services.AddTransient<IHashService, HashService> ();
            services.AddTransient<IRandomService, RandomService> ();
        }
    }
}
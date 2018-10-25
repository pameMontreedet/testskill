using System;
using AppLib.Core;
using Microsoft.Extensions.DependencyInjection;

namespace AppLib.Modules.Card {
    public class CardModule : BaseModule {
        public override void RegisterServices (IServiceCollection services) {
            services.AddTransient<ICardService, CardService>();
            services.AddTransient<ICardStorage, CardStorage>();
        }
    }
}
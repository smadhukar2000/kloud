using DACLayer.Interfaces;
using KloudAZFunctions.Implementations;
using KloudAZFunctions.Interfaces;
using Microsoft.Azure.WebJobs.Host.Config;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace KloudAZFunctions.DI
{
    public class InjectConfiguration : IExtensionConfigProvider
    {
        private IServiceProvider _serviceProvider;

        public void Initialize(ExtensionConfigContext context)
        {
            var services = new ServiceCollection();
            RegisterServices(services);
            _serviceProvider = services.BuildServiceProvider(true);

            context
                .AddBindingRule<InjectAttribute>()
                .BindToInput<dynamic>(i => _serviceProvider.GetRequiredService(i.Type));
        }
        private void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IDACLayer, DACLayer.Implementations.DACLayer>();
            services.AddSingleton<ITransactionBL, TransactionBL>();
        }
    }
}

// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop
{
    using Funq;
    using Serilog;
    using ServiceStack.Discovery.Consul;
    using ServiceStack.Extras.Serilog;
    using ServiceStack.IntroSpec;
    using ServiceStack.IntroSpec.ServiceCop.Core;
    using ServiceStack.IntroSpec.ServiceCop.ServiceModel;
    using ServiceStack.Logging;
    using ServiceStack.Validation;

    public class AppHost : AppSelfHostBase
    {
        private readonly string externalUrl;
        private readonly ILogger logger;

        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        /// <param name="externalUrl">the service url</param>
        /// <param name="logger">serilog logger</param>
        public AppHost(string externalUrl, ILogger logger) : base("ServiceStack.IntroSpec.ServiceCop", typeof(ServiceCopService).Assembly)
        {
            this.externalUrl = externalUrl.ThrowIfNullOrEmpty(nameof(externalUrl));
            this.logger = logger.ThrowIfNull(nameof(logger));
        }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container">the ioc container</param>
        public override void Configure(Container container)
        {
            LogManager.LogFactory = new SerilogFactory(logger);

            SetConfig(new HostConfig
            {
                // the url:port that other services will use to access this one
                WebHostUrl = externalUrl,
                EnableFeatures = Feature.All.Remove(Feature.Soap)
            });

            Plugins.Add(new ConsulFeature());
            Plugins.Add(new ValidationFeature());
            Plugins.Add(new IntroSpecFeature());
            Plugins.Add(new ServiceCopValidationFeature(RuleConfig.Load()));

            RegisterServicesInAssembly(typeof(ServiceCopService).Assembly);

            // TODO Replace with ConsulServiceProvider
            RegisterAs<DummyServiceProvider, IServiceProvider<ServiceDetail>>();
            RegisterAs<DefaultSpecProvider, ISpecProvider>();
            RegisterAs<ServiceValidator, IServiceValidator>();

            // TODO Remove, for testing only
            Register(new TestDataService());

            // validator for introspec servicecop requests
            container.RegisterValidators(typeof(ValidateServiceRequestValidator).Assembly);
        }
    }
}

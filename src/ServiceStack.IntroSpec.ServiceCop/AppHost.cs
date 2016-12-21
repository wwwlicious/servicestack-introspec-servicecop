namespace ServiceStack.IntroSpec.ServiceCop
{
    using Funq;
    using Serilog;
    using ServiceStack.Discovery.Consul;
    using ServiceStack.IntroSpec;
    using ServiceStack.IntroSpec.Extensions;
    using ServiceStack.IntroSpec.ServiceCop.ServiceInterface;
    using ServiceStack.IntroSpec.ServiceCop.ServiceModel;
    //using ServiceStack.Logging.Serilog;
    using ServiceStack.Validation;

    public class AppHost : AppSelfHostBase
    {
        private readonly string externalUrl;
        private readonly ILogger logger;

        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        /// <param name="externalUrl"></param>
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
        public override void Configure(Container container)
        {
            SetConfig(new HostConfig
            {
                // the url:port that other services will use to access this one
                WebHostUrl = externalUrl,
                EnableFeatures = Feature.All.Remove(Feature.Soap)
            });

            Plugins.Add(new ConsulFeature());
            Plugins.Add(new ValidationFeature());
            Plugins.Add(new IntroSpecFeature());

            RegisterServicesInAssembly(typeof(ServiceCopService).Assembly);

            // TODO Replace with ConsulServiceProvider
            RegisterAs<DummyServiceProvider, IServiceProvider<ServiceDetail>>();
            RegisterAs<DefaultSpecProvider, ISpecProvider>();
            RegisterAs<ServiceValidator, IServiceValidator>();

            // TODO Remove, for testing only
            Register(new TestDataService());

            container.RegisterValidators(typeof(ValidateServiceRequestValidator).Assembly);
        }
    }
}

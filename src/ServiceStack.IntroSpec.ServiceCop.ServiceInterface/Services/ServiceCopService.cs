namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using System.IO;
    using ServiceStack.Discovery.Consul;
    using ServiceStack.IntroSpec.Models;
    using ServiceStack.IntroSpec.ServiceCop.ServiceModel;

    public class ServiceCopService : Service
    {
        public IServiceProvider<ServiceDetail> Provider { get; set; }

        public IServiceValidator ServiceValidator { get; set; }

        public ISpecProvider SpecProvider { get; set; }

        public object Any(GetDiscoveryServicesRequest request)
        {
            return new GetDiscoveryServicesResponse
            {
                Services = Provider.GetServices()
            };
        }

        public object Any(ValidateServiceRequest request)
        {
            string serviceUrl = null;
            ApiDocumentation documentation = null;

            if (!request.ServiceId.IsNullOrEmpty())
            {
                var service = ConsulClient.GetServices(request.ServiceId);
                if (service.Length != 1)
                    new ServiceNotFoundException(request.ServiceId);
                serviceUrl = service[0].ServiceAddress;
            }
            else if (request.ServiceUrl.IsWellFormedOriginalString())
            {
                serviceUrl = request.ServiceUrl.ToString();
            }

            if (!serviceUrl.IsNullOrEmpty())
            {
                // get introspec result, need specific url, rather than gateway provided one
                var apiDocumentation = SpecProvider.GetSpec(serviceUrl);
                if (apiDocumentation == null) throw new IntrospecNotFoundException(serviceUrl);
                documentation = apiDocumentation;
            }
            else
            {
                documentation = request.IntroSpecJson.FromJson<ApiDocumentation>();
                if(documentation == null) throw new InvalidDataException("The json was not a valid IntroSpec.ApiDocumentation format");
            }

            // pass to validator
            return new ValidateServiceResponse
            {
                Result = new SpecValidationResult().PopulateWith(ServiceValidator.Validate(documentation))
            };
        }

        //public object Any(RegisterServiceRequest request) { }

        //public object Any(CreateServiceCopExceptionRequest request) { }

        //public object Any(AuthoriseServiceOverrideRequest request) { }
    }

    public class SnapshotService : Service
    {
        //public object Any(GetServiceCopSnapshot request) { }

        //public object Any(CreateServiceCopSnapshot request) { }

        //public object Any(RemoveServiceCopSnapshot request) { }
    }
}
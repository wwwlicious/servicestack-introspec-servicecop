namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.Discovery.Consul;
    using ServiceStack.IntroSpec.ServiceCop.ServiceModel;

    public class DummyServiceProvider : IServiceProvider<ServiceDetail>
    {
        private ServiceDetail serviceDetail;

        public ServiceDetail[] GetServices()
        {
            serviceDetail = new ServiceDetail { Id = "1", Name = "Test", ServiceUrl = "http://introspec.servicestack.net/" };
            return new[] { serviceDetail };
        }

        public ServiceDetail GetService(string serviceId)
        {
            return serviceDetail;
        }

        public void SuspendService(string serviceId, string maintenanceMessage)
        {
            // first need to find node (agent) that manages service
            http://127.0.0.1:8500/v1/catalog/service/api?tags=GetDiscoveryServiceRequest

            /*
             * {
Node: "X1-Win10",
Address: "127.0.0.1",
ServiceID: "ss-ServiceStack.IntroSpec.ServiceCop-eebca8c7-f8f4-4a07-a7d3-581fff28ca48",
ServiceName: "api",
ServiceTags: [
"ss-version-1.0",
"GetDiscoveryServicesRequest",
"ValidateServiceRequest"
],
ServiceAddress: "http://localhost:8088",
ServicePort: 8088,
ServiceEnableTagOverride: false,
CreateIndex: 2131,
ModifyIndex: 2143
}
             * */

            // next need to suspend service on agent with validation failure message
            var request = new SuspendServiceRequest { ServiceId = serviceId , Reason = maintenanceMessage };
            "127.0.0.1".AppendUrlPaths(request.ToUrl()).PutToUrl(null, responseFilter: response =>
            {
                //if(response.IsErrorResponse() throw response.GetResponseStatus());)
            });
        }

        public void EnableService(string serviceId)
        {
            // re-enables a service
            var request = new EnableServiceRequest { ServiceId = serviceId };
            "127.0.0.1".AppendUrlPaths(request.ToUrl()).PutToUrl(null, responseFilter: response =>
            {
                // throw if error response
            });
        }
    }
}
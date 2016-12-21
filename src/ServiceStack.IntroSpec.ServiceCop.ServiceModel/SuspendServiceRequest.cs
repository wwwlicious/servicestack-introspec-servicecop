namespace ServiceStack.IntroSpec.ServiceCop.ServiceModel
{
    using ServiceStack.DataAnnotations;

    [Route("/v1/agent/service/maintenance/{ServiceId}", "PUT")]
    [Exclude(Feature.ServiceDiscovery)]
    public class SuspendServiceRequest : IPut
    {
        public string ServiceId { get; set; }
        public string Reason { get; set; }
        public bool Enable => false;
    }
}
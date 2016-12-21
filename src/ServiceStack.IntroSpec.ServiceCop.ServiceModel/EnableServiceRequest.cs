namespace ServiceStack.IntroSpec.ServiceCop.ServiceModel
{
    using ServiceStack.DataAnnotations;

    [Route("/v1/agent/service/maintenance/{ServiceId}", "PUT")]
    [Exclude(Feature.ServiceDiscovery)]
    public class EnableServiceRequest : IPut
    {
        public string ServiceId { get; set; }
        public bool Enable => true;
    }
}

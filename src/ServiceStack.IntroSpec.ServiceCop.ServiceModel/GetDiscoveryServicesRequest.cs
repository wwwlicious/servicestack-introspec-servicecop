namespace ServiceStack.IntroSpec.ServiceCop.ServiceModel
{
    using ServiceStack.DataAnnotations;

    [Exclude(Feature.ServiceDiscovery)]
    public class GetDiscoveryServicesRequest : IReturn<GetDiscoveryServicesResponse>
    {
    }
}
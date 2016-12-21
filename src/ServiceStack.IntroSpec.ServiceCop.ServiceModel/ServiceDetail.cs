namespace ServiceStack.IntroSpec.ServiceCop.ServiceModel
{
    public class ServiceDetail
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public HealthStatus Health { get; set; }

        public ServiceStatus OperationalStatus { get; set; }

        public string ServiceUrl { get; set; }
    }
}
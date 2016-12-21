namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using System;

    public class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException(string serviceId, string message = "The service could not be found")
            : base(message)
        {
            ServiceId = serviceId;
        }

        public string ServiceId { get; set; }
    }
}
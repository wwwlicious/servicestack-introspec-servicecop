namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using System;

    public class SnapshotSummary
    {
        public string Id { get; set; }

        public string ServiceId { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
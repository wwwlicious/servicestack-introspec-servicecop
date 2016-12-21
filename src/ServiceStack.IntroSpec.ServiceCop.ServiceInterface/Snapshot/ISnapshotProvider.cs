namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using System;

    /// <summary>
    /// creates a snapshot of the service definition
    /// used to enforce non-breaking changes by comparing prev snapshots
    /// </summary>
    public interface ISnapshotProvider
    {
        SnapshotSummary Create(Uri serviceUri);

        SnapshotValidationResult Compare(SnapshotSummary @old, SnapshotSummary @new);
    }
}
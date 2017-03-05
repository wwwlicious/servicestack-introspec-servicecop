// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ServiceStack.IntroSpec.Models;

    /// <summary>
    /// creates a snapshot of the service definition
    /// used to enforce non-breaking changes by comparing against previous snapshots
    /// </summary>
    public interface ISnapshotProvider
    {
        /// <summary>
        /// Creates a new set of snapshots for an ApiDocumentation object
        /// </summary>
        /// <param name="apiDocumentation">The api documentation</param>
        /// <returns>the snapshots created</returns>
        SnapshotSummary[] Create(ApiDocumentation apiDocumentation);

        /// <summary>
        /// Loads an existing service snapshot
        /// </summary>
        /// <param name="apiDocumentation">The ApiDocumentation</param>
        /// <returns>The snapshots</returns>
        SnapshotSummary[] Load(ApiDocumentation apiDocumentation);
    }

    public class SnapshotProvider : ISnapshotProvider
    {
        public SnapshotProvider()
        {
            // TODO for demo only add some snapshots
            this.Summaries = new List<SnapshotSummary>
                            {
                                new SnapshotSummary()
                                    {
                                        Id = "DtoName1",
                                        Created = DateTime.Today,
                                        ServiceId = "Service1",
                                        Version = "1.1",
                                        Resource = new ApiResourceDocumentation
                                                       {
                                                           TypeName = "DtoName1",
                                                           Actions = new[] { new ApiAction() { } },
                                                           AllowMultiple = false,
                                                           Category = "TestCategory",
                                                           Description = "TestDescription",
                                                           Notes = "Test Notes",
                                                           Properties = new[]
                                                                            {
                                                                                new ApiPropertyDocumention
                                                                                    {
                                                                                        Id = "TestProp",
                                                                                        Description = "TestDesc",
                                                                                        Notes = "Test Notes"
                                                                                        ,
                                                                                        AllowMultiple = false,
                                                                                        ClrType = typeof(SnapshotSummary),
                                                                                        Contraints =
                                                                                            new PropertyConstraint()
                                                                                                {
                                                                                                    Type = ConstraintType.List,
                                                                                                    Values = new[] { "One", "Two", "Three" }
                                                                                                }
                                                                                        ,
                                                                                        EmbeddedResource = new ApiResourceDocumentation(), // nested type
                                                                                        ExternalLinks = new[] { "No idea what this is, lookup" },
                                                                                        IsRequired = false,
                                                                                        ParamType = "System.String",
                                                                                        Title = "Dto Name 1"
                                                                                    }
                                                                            }
                                                       }
                                    },
                            };
        }

        private List<SnapshotSummary> Summaries { get; set; }

        public SnapshotSummary[] Create(ApiDocumentation apiDocumentation)
        {
            // get the summary data from apidocumentation and add an entry for each apiresourcedocumentation
            var serviceUrl = apiDocumentation.ApiBaseUrl;
            var apiVersion = apiDocumentation.ApiVersion;
            var resources = apiDocumentation.Resources;

            var snapshots =
                resources.Select(
                    x =>
                        new SnapshotSummary
                        {
                            Id = x.TypeName,
                            Created = DateTime.UtcNow,
                            Resource = x,
                            ServiceId = serviceUrl,
                            Version = apiVersion
                        });
            Summaries.AddRange(snapshots);
            return Summaries.ToArray();
        }

        public SnapshotSummary[] Load(ApiDocumentation apiDocumentation)
        {
            // first look for existing service url
            var baseUrl = apiDocumentation.ApiBaseUrl;

            // if no exact matches, look for each resource name
            var resources = apiDocumentation.Resources;
            var found = resources.Select(x => Lookup(x.TypeName)).OrderByDescending(x => x.Version);

            var apiVersion = apiDocumentation.ApiVersion;

            // Note, will need to match version for all snapshots loaded to 
            // allow for explicit removal of DTO's between versions where older snapshots
            // may exist.
            return found.ToArray();
        }

        /// <summary>
        /// Gets an existing snapshot summary, only returns the latest
        /// </summary>
        /// <param name="resourceKey">the snapshot id, this is the DTO name which should be globally unique</param>
        /// <returns>The latest version of s snapshot summary, ordered by version</returns>
        private SnapshotSummary Lookup(string resourceKey)
        {
            // return any snapshots were the type (key) matches
            // we do not include namespaces in the key lookup, only DTO names
            var results = Summaries.Where(x => x.Id.EqualsIgnoreCase(resourceKey)).OrderByDescending(x => x.Version).SingleOrDefault();
            return results;
        }
    }
}
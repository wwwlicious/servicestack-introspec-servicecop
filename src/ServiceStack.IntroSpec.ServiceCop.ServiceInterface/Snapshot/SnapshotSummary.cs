// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using System;
    using Semver;
    using ServiceStack.IntroSpec.Models;

    public class SnapshotSummary
    {
        /// <summary>
        /// The snapshot id (the dto name)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// the service it was registered with
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// The date of the snapshot
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The api resource
        /// </summary>
        public ApiResourceDocumentation Resource { get; set; }

        /// <summary>
        /// The version of ApiDocumentation it was registered with
        /// that can handle accurate sorts and enforce with servicecop validation?
        /// </summary>
        public SemVersion Version { get; set; }
    }
}
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.ServiceModel
{
    using System;

    /// <summary>
    /// Validates a service
    /// </summary>
    /// <remarks>specific only one value in order of precedence: id > url > json</remarks>
    public class ValidateServiceRequest : IReturn<ValidateServiceResponse>
    {
        /// <summary>
        /// Validates a service using the consul service registration Id
        /// </summary>
        /// <remarks>Takes precedence if specified</remarks>
        public string ServiceId { get; set; }

        /// <summary>
        /// Validates a service against it's /spec endpoint
        /// </summary>
        /// <remarks>Used second if no <seealso cref="ServiceId"/> is specified</remarks>
        public Uri ServiceUrl { get; set; }

        /// <summary>
        /// Validates a service using the /spec json output
        /// </summary>
        /// <remarks>Used last if no <seealso cref="ServiceId"/> or <seealso cref="ServiceUrl"/> is specified</remarks>
        public string IntroSpecJson { get; set; }
    }
}
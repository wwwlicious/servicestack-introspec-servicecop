// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

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

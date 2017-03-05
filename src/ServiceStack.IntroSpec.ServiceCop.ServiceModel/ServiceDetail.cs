// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

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
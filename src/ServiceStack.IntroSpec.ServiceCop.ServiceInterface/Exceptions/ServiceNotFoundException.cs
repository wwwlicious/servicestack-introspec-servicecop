// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

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
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using System;

    public class IntrospecNotFoundException : Exception
    {
        public IntrospecNotFoundException(string introSpecUrl) : base("The service spec could not be loaded, check that ApiSpecFeature is installed and the service url is correct")
        {
            IntroSpecUrl = introSpecUrl;
        }

        public string IntroSpecUrl { get; set; }
    }
}
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.IntroSpec.DTO;
    using ServiceStack.IntroSpec.Models;

    public class DefaultSpecProvider : ISpecProvider
    {
        /// <summary>
        /// Retrieves an introspec document from a service url
        /// </summary>
        /// <param name="serviceUrl">the service base url</param>
        /// <returns>the introspec specification</returns>
        public ApiDocumentation GetSpec(string serviceUrl)
        {
            var specRequest = new SpecRequest().ToUrl();
            var introSpecUrl = serviceUrl.AppendUrlPaths(specRequest);
            var specResponse = introSpecUrl.GetJsonFromUrl().FromJson<SpecResponse>();
            return specResponse?.ApiDocumentation;
        }
    }
}
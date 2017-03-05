// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.IntroSpec.Models;

    public class ApiDocumentationCompare
    {
        public ApiDocumentation Original { get; set; }
        public ApiDocumentation Instance { get; set; }
    }

    public class ApiResourceDocumentationCompare
    {
        public ApiResourceDocumentation Original { get; set; }
        public ApiResourceDocumentation Instance { get; set; }
    }

    public class ApiResourceTypeCompare
    {
        public IApiResourceType Original { get; set; }
        public IApiResourceType Instance { get; set; }
    }
}
namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using System.Collections.Generic;
    using System.Linq;
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
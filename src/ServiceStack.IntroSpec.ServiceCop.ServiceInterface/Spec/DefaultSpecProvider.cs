namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.IntroSpec.DTO;
    using ServiceStack.IntroSpec.Models;

    public class DefaultSpecProvider : ISpecProvider
    {
        /// <summary>
        /// Retrieves a spec from a service
        /// </summary>
        /// <param name="serviceUrl">the service base url</param>
        /// <returns>the introspec speicification</returns>
        public ApiDocumentation GetSpec(string serviceUrl)
        {
            var specRequest = new SpecRequest().ToUrl();
            var introSpecUrl = serviceUrl.AppendUrlPaths(specRequest);
            var specResponse = introSpecUrl.GetJsonFromUrl().FromJson<SpecResponse>();
            return specResponse?.ApiDocumentation;
        }
    }
}
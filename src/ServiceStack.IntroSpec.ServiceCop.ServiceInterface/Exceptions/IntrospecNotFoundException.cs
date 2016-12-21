namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
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
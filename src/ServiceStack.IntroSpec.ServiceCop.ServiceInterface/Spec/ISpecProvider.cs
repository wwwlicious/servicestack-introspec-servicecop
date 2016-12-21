namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.IntroSpec.Models;

    public interface ISpecProvider
    {
        ApiDocumentation GetSpec(string serviceUrl);
    }

    public interface IRule
    {
        
    }
}
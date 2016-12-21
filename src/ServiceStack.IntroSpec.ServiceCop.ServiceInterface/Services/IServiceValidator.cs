namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.FluentValidation;
    using ServiceStack.FluentValidation.Results;
    using ServiceStack.IntroSpec.Models;

    public interface IServiceValidator
    {
        IValidator[] Validators { get; }

        ValidationResult Validate(ApiDocumentation specResponse);
    }
}
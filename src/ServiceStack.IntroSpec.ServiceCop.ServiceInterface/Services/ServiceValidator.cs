namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using System.Collections.Generic;
    using ServiceStack.FluentValidation;
    using ServiceStack.FluentValidation.Results;
    using ServiceStack.IntroSpec.DTO;
    using ServiceStack.IntroSpec.Models;
    using ServiceStack.IntroSpec.Validators;

    public class ServiceValidator : IServiceValidator
    {
        private readonly List<IValidator> validators = new List<IValidator>();

        public IValidator[] Validators => validators.ToArray();

        public ApiDocumentationValidator Validator { get; } = new ApiDocumentationValidator();

        public ServiceValidator()
        {
            // TODO make validators configurable
            validators.Add(new RequestNameValidator());
        }

        public ValidationResult Validate(ApiDocumentation specResponse)
        {
            return Validator.Validate(specResponse);
        }
    }
}
namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    /// <summary>
    /// All Request DTO's must have an associated validator
    /// Why?
    /// This is considered best practice to decouple validation logic from endpoints
    /// and promote a consistent response/status codes (the standard validationexception from servicestack)
    /// </summary>
    public class EnforceRequestValidationValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public EnforceRequestValidationValidator()
        {
            RuleFor(x => x.HasValidator).Must(x => x.HasValue && x.Value)
                .WithErrorCode("MissingValidator")
                .WithMessage("The request must have a registered AbstractValidator<TRequest>");
        }
    }
}
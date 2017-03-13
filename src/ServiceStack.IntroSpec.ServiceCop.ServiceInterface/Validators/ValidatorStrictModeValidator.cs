namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    /// <summary>
    /// If strict mode is enabled, all Request DTO's must have an associated validator
    /// Why?
    /// This is considered best practice to avoid putting validation logic in endpoints 
    /// and promote a consistent response and status codes (the standard validationexception from servicestack)
    /// </summary>
    public class ValidatorStrictModeValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public ValidatorStrictModeValidator()
        {
            RuleFor(x => x.HasValidator).NotNull().Must(x => x.Value);
        }
    }
}
namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    /// <summary>
    /// The request DTO type names when split with CamelCase must contain a min number of words
    /// Why?
    /// In a global catalog of DTO's registered via service discovery, having DTO names which are
    /// generic or not specific enough such as GetStatus or PostResult will be meaningless outwith
    /// their particular service.
    /// This validator enforces the use of longer, more specific DTO names that retain semantic meaning
    /// when presented alongside thousands or other DTO names for discovery and reduces the changes
    /// of name collisions between services
    /// </summary>
    public class RequestNameLengthValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public RequestNameLengthValidator(int minRequestNameWords)
        {
            // TODO optional, potential restricted dto naming or word list?
            RuleFor(x => x.TypeName).NotEmpty().MinimumWords(minRequestNameWords, true)
                .WithMessage($"The request name must have a minimum of {minRequestNameWords} words");

        }
    }
}
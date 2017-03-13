namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    /// <summary>
    /// Checks the documentation that will be generated for the DTO meets certain criteria
    /// </summary>
    public class RequestDocumentationValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public RequestDocumentationValidator()
        {
            // TODO Make configurable via rules
            // TODO Split into different validators or turn on/off via config?

            // Titles should be formatted correctly? GetMyRequest - Get My Request
            var minimumTitleWords = 1;
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotEqual(x => x.TypeName)
                .WithMessage("The request Title should be different from the DTO type name")
                .MinimumWords(minimumTitleWords)
                .WithMessage($"The request Title should be at least {minimumTitleWords} words");

            // A category must be defined for the DTO
            RuleFor(x => x.Category).NotEmpty();

            // The description should not repeat the title and be a minimum number of words
            var minimumWords = 3;
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotEqual(x => x.Title)
                .WithMessage("The request Description should be different from the Title")
                .MinimumWords(minimumWords)
                .WithMessage($"The request Description should be at least {minimumWords} words")
                .WithSeverity(Severity.Warning);

            RuleFor(x => x.Tags).NotEmpty().NoDuplicates();
        }
    }
}
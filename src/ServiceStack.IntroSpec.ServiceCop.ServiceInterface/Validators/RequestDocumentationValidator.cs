namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    /// <summary>
    /// Checks the documentation that will be generated for the DTO meets certain criteria
    /// </summary>
    public class RequestDocumentationValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public RequestDocumentationValidator(RequestDocumentationRule rule)
        {
            // Titles should be formatted correctly? GetMyRequest - Get My Request
            var minimumTitleWords = rule.MinimumTitleWords;
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotEqual(x => x.TypeName).When(x => rule.TitleMustNotRepeatDtoType)
                .WithMessage("The request Title should be different from the DTO type name")
                .MinimumWords(minimumTitleWords)
                .WithMessage($"The request Title should be at least {minimumTitleWords} words");

            // A category must be defined for the DTO
            RuleFor(x => x.Category).NotEmpty();

            // The description should not repeat the title and be a minimum number of words
            var minimumWords = rule.MinimumDescriptionWords;
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotEqual(x => x.Title)
                .When(x => rule.DescriptionMustNotRepeatTitle)
                .WithMessage("The request Description should be different from the Title")
                .MinimumWords(minimumWords)
                .WithMessage($"The request Description should be at least {minimumWords} words")
                .WithSeverity(Severity.Warning);

            RuleFor(x => x.Tags).NotEmpty().NoDuplicates().When(x => rule.TagsMustNotBeRepeated)
                .WithName("DuplicateTags")
                .WithMessage("Some of the documentation tags are repeated");
        }
    }
}
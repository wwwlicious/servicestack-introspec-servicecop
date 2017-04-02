namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;
    using ServiceStack.Support.Markdown;

    /// <summary>
    /// The request and response DTO type names should match (excluding any postfixes)
    /// Why?
    /// To promote consistency and discoverability 
    /// For example GetSomething{Request} should return GetSomething{Response}
    /// where the {} indicates the ignored dto postfix
    /// </summary>
    public class RequestResponsePairValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public RequestResponsePairValidator(string requestPostfix, string responsePostfix)
        {
            // Only applies to non-void (not one-way) requests
            When(x => x.ReturnType != null, () =>
            {
                RuleFor(x => x).Must((left, right) =>
                        left.TypeName.TrimIfEndingWith(requestPostfix)
                            .Equals(right.ReturnType.TypeName.TrimIfEndingWith(responsePostfix)))
                    .WithMessage("The request name must match the response name")
                    .WithErrorCode("RequestResponsePair");
            });
        }
    }
}
namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;
    using ServiceStack.IntroSpec.Validators;

    public class ApiDocumentationValidator : AbstractValidator<ApiDocumentation>
    {
        public ApiDocumentationValidator()
        {
            RuleFor(x => x.Contact).SetValidator(new ApiContactValidator());
            RuleFor(x => x.ApiVersion).SemVer();
            When(x => !x.ApiBaseUrl.IsNullOrEmpty(), () => RuleFor(a => a.ApiBaseUrl).Url());
            RuleForEach(x => x.Resources).SetValidator( new RequestNameValidator());
        }
    }
}
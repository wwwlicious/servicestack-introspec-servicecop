namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    public class ApiResourceValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public ApiResourceValidator()
        {
            var apiPropertyValidator = new ApiPropertyValidator();
            //RuleForEach(x => x.Properties).SetValidator()
        } 
    }
}
namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules
{
    using System;
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    public class DtoRequestPostfixValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public DtoRequestPostfixValidator(string postfix, Severity severity) 
        {
            RuleFor(x => x.TypeName)
                .Must(x => x.EndsWith(postfix, StringComparison.InvariantCulture))
                .WithName(RuleIds.DtoRequestPostfix)
                .WithMessage("The request DTO `{0}` does not have the required postfix `{1}`", x => x.TypeName, x => postfix)
                .WithSeverity(severity);
        }
    }
}
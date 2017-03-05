namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    public class DtoRequestPrefixRule : AbstractRule
    {
        public DtoRequestPrefixRule()
        {
            Id = RuleIds.DtoRequestPrefix;
            Category = "Naming";
        }

        public override void CreateValidator()
        {
            Validator = new DtoRequestPrefixValidator(Severity);
        }
    }

    public class DtoRequestPrefixValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public DtoRequestPrefixValidator(Severity severity)
        {
            RuleFor(x => x)
                .Must(x => x.TypeName.StartsWithAny(x.Actions.Select(a => a.Verb), StringComparison.Ordinal))
                .WithName(RuleIds.DtoRequestPostfix)
                .WithMessage("The request DTO `{0}` should have at least one of the allowed verb prefixes `{1}`", x => x.TypeName, x => x.Actions.Select(a => a.Verb).Join(","))
                .WithSeverity(severity);
        }
    }

    public static class StringExtensions
    {
        public static bool StartsWithAny(this string value, IEnumerable<string> values, StringComparison comparison)
        {
            return values.Any(v => value.StartsWith(v, comparison));
        }
    }
}
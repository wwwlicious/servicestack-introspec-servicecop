// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 
namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules
{
    using System;
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    public class DtoRequestPostfixRule : AbstractRule
    {
        public DtoRequestPostfixRule()
        {
            Id = RuleIds.DtoRequestPostfix;
            Category = "Naming";
            Value = "Request";
        }

        public string Value { get; set; }

        public override void CreateValidator()
        {
            Validator = new DtoRequestPostfixValidator(Value, Severity);
        }
    }

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

    public class RuleIds
    {
        public const string DtoRequestPostfix = "IntroSpec.DtoRequestPostfix";
        public const string DtoRequestPrefix = "IntroSpec.DtoRequestPrefix";
    }
}
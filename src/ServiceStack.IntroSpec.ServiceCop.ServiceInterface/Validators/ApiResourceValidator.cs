// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using System.Linq;
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    public class ApiResourceValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public ApiResourceValidator(RuleConfig ruleConfig)
        {
            // Add validators for all rules applicable to ApiResourceDocumentation types
            var validators = ruleConfig.Rules.Select(x => x.CreateValidator()).ToArray();
            foreach (var validator in validators.ForValidating<ApiResourceDocumentation>())
            {
                RuleFor(x => x).SetValidator(validator).WithName("ApiResourceDocumentation");
            }
        }
    }

    // rules 

    // DTO's with Enforce Validators mode must have a validator (HasValidator prop)
    // TODO Add a custom message provider to append help doc url to messages
}
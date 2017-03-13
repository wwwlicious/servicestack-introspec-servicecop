// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;
    using ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules;
    using ServiceStack.NativeTypes;

    public class ApiResourceValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public ApiResourceValidator(RuleConfig ruleConfig)
        {
            // Add validators for all rules applicable to ApiResourceDocumentation types
            var resourceRules = ruleConfig.Rules.Where(x => x.Validator.CanValidateInstancesOfType(typeof(ApiResourceDocumentation)));
            foreach (var resourceRule in resourceRules)
            {
                RuleFor(x => x).SetValidator(resourceRule.Validator as IValidator<ApiResourceDocumentation>).WithName("ApiResourceDocumentation");
            }
        }
    }

    // rules 

    // DTO's with Enforce Validators mode must have a validator (HasValidator prop)
    // TODO Add a custom message provider to append help doc url to messages
}
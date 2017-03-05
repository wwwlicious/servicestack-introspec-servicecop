// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using System.Linq;
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;
    using ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules;

    public class ApiResourceValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public ApiResourceValidator(RuleConfig ruleConfig)
        {
            var resourceRules = ruleConfig.Rules.Where(x => x.Validator.CanValidateInstancesOfType(typeof(ApiResourceDocumentation)));


            foreach (var resourceRule in resourceRules)
            {
                // NOTE pre postfix are req/response independent, should be diff rules 
                // parent types are not the same though
                RuleFor(x => x).SetValidator(resourceRule.Validator as IValidator<ApiResourceDocumentation>).WithName("ApiResourceDocumentation");
            }

            // var apiPropertyValidator = new ApiPropertyValidator();
            // RuleFor(x => x).SetValidator(new ApiPropertyValidator());
        }
    } 
}
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using System.Collections.Generic;
    using System.Linq;
    using ServiceStack.FluentValidation;
    using ServiceStack.FluentValidation.Results;
    using ServiceStack.IntroSpec.Models;
    using ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules;

    public class ServiceValidator : IServiceValidator
    {
        public ServiceValidator()
        {
            // Validators are configured/enabled via an introspec.json file
            var config = RuleConfig.Load();

            // this is the hardcoded version, perhaps that is enough in which case above is not needed.
            Validator = new ApiDocumentationValidator(config);
        }

        public ApiDocumentationValidator Validator { get; }

        public ValidationResult Validate(ApiDocumentation specResponse)
        {
            return Validator.Validate(specResponse);
        }
    }
}
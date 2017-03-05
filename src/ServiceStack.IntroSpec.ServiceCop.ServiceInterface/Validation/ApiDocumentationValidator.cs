// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using Semver;
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;
    using ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules;
    using ServiceStack.IntroSpec.Validators;

    public class ApiDocumentationValidator : AbstractValidator<ApiDocumentation>
    {
        public ApiDocumentationValidator(RuleConfig ruleConfig)
        {
            // A contact email should always be provided so that owner can get validation failure notifications
            RuleFor(x => x.Contact).SetValidator(new ApiContactValidator());

            // enforces a strict semver 2.0.0 spec so that we can get reliable sorting for snapshots
            SemVersion semver;
            RuleFor(x => x.ApiVersion).Must(x => SemVersion.TryParse(x, out semver, true))
                .WithErrorCode("ApiVersion")
                .WithMessage("The api version `{0}` must be in semver v2.0.0 format to allow reliable sorting, see http://semver.org for details", x => x.ApiVersion);

            // seems obvious but no service url or badly formed urls allowed
            When(x => !x.ApiBaseUrl.IsNullOrEmpty(), () => RuleFor(a => a.ApiBaseUrl).Url());

            // the main validators for each dto (ApiResourceDocumentation) are here
            RuleFor(x => x.Resources).SetCollectionValidator(new ApiResourceValidator(ruleConfig));
            //RuleForEach(x => x.Resources).SetValidator(new ApiResourceValidator(ruleConfig));
        }
    }
}
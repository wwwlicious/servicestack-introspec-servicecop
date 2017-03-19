// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.ServiceCop.ServiceModel;

    public class ValidateServiceRequestValidator : AbstractValidator<ValidateServiceRequest>
    {
        public ValidateServiceRequestValidator()
        {
            // order of precedence id > url > json 
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.ServiceId).NotEmpty().When(x => x.ServiceUrl == null && x.IntroSpecJson == null);
            RuleFor(x => x.ServiceUrl).NotEmpty().Must(x => x.IsAbsoluteUri).When(x => x.ServiceId == null && x.IntroSpecJson == null);
            RuleFor(x => x.IntroSpecJson).NotEmpty().When(x => x.ServiceId == null && x.ServiceUrl == null);
        }
    }
}
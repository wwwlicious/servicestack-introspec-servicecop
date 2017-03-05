// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.FluentValidation;

    public class StringTokenValidator : AbstractValidator<string>
    {
        public StringTokenValidator()
        {
            // should tokenize caps into words
            // single word requests should fail (and produce service warnings)
            // why?
            // because it is likely that it would clash or not be meaningful
            // amongst hundreds of other services
            // ex. OrderRequest could end up in purchase orders, sales orders, works orders etc
            // need specific names

            // must have more than two words in a DTO name
            RuleFor(x => x).Must(x => x.SplitCamelCase().Split(' ').Length > 2);
        }
    }
}

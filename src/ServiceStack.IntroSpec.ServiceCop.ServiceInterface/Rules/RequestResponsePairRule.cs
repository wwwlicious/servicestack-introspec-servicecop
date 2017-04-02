// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 
namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using ServiceStack.FluentValidation;

    public class RequestResponsePairRule : AbstractRule
    {
        public RequestResponsePairRule()
        {
            Category = RuleCategories.Naming;
        }

        public override IValidator CreateValidator()
        {
            return new RequestResponsePairValidator(RequestPostfix, ResponsePostfix);
        }

        public string ResponsePostfix { get; set; } = "Response";

        public string RequestPostfix { get; set; } = "Request";
    }
}
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 
namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using ServiceStack.FluentValidation;

    public class RequestNameLengthRule : AbstractRule
    {
        public RequestNameLengthRule()
        {
            Category = RuleCategories.Naming;
        }

        public override IValidator CreateValidator()
        {
            return new RequestNameLengthValidator(MinimumRequestNameWords);
        }

        public int MinimumRequestNameWords { get; set; } = 3;
    }
}
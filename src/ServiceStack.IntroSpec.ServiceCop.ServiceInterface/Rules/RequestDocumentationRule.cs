// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using ServiceStack.FluentValidation;

    public class RequestDocumentationRule : AbstractRule
    {
        public RequestDocumentationRule()
        {
            Id = RuleIds.RequestDocumentationRule;
            Category = RuleCategories.Documentation;
        }

        public int MinimumTitleWords { get; set; } = 3;
        public int MinimumDescriptionWords { get; set; } = 3;
        public bool TitleMustNotRepeatDtoType { get; set; } = true;
        public bool DescriptionMustNotRepeatTitle { get; set; } = true;
        public bool TagsMustNotBeRepeated { get; set; } = true;

        public override IValidator CreateValidator()
        {
            return new RequestDocumentationValidator(this);
        }
    }
}
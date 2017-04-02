﻿// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 
namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using ServiceStack.FluentValidation;

    public class EnforceRequestValidationRule : AbstractRule
    {
        public EnforceRequestValidationRule()
        {
            Category = RuleCategories.Design;
        }

        public override IValidator CreateValidator()
        {
            return new EnforceRequestValidationValidator();
        }
    }
}
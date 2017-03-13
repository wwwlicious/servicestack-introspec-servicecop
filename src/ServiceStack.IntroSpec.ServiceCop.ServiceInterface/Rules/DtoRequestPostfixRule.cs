// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 
namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules
{
    public class DtoRequestPostfixRule : AbstractRule
    {
        public DtoRequestPostfixRule()
        {
            Id = RuleIds.DtoRequestPostfix;
            Category = "Naming";
            Value = "Request";
        }

        public string Value { get; set; }

        public override void CreateValidator()
        {
            Validator = new DtoRequestPostfixValidator(Value, Severity);
        }
    }
}
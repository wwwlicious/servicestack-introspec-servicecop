// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 
namespace ServiceStack.IntroSpec.ServiceCop.Tests.ServiceCop.ServiceInterface.Rules
{
    using FluentAssertions;
    using ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules;
    using Xunit;

    public class RuleConfigTests
    {
        [Fact]
        public void Can_Initialise()
        {
            var ruleConfig = RuleConfig.Load();
            ruleConfig.Rules.Should().ContainSingle(x => x.Id == RuleIds.DtoRequestPostfix);
        }
    }
}
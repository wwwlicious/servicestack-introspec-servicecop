// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.Tests
{
    using FluentAssertions;
    using ServiceStack.IntroSpec.Models;
    using ServiceStack.IntroSpec.ServiceCop.ServiceInterface;
    using ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules;
    using ServiceStack.Testing;
    using Xunit;

    public class RuleTests
    {
        [Fact]
        public void SerializeRule()
        {
            var config = RuleConfig.Load();
            config.DtoRequestPostfixRule.Enabled = true;
            config.Save();

            var runner = new ServiceCopValidationFeature(config);
            var basicAppHost = new BasicAppHost();
            
            runner.Register(basicAppHost);
            
            var result = config.DtoRequestPostfixRule.Validator.Validate(new ApiResourceDocumentation { TypeName = "woo" });

            result.IsValid.Should().BeFalse();
            var failure = result.Errors.Should().ContainSingle(x => x.PropertyName.EqualsIgnoreCase("TypeName")).Subject;
            
            failure.ErrorMessage.Should().Be("The request DTO `woo` does not have the required postfix `Request`");
        }
    }
}
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 
namespace ServiceStack.IntroSpec.ServiceCop.Tests.ServiceCop.ServiceInterface
{
    using FluentAssertions;
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.ServiceCop.ServiceInterface;
    using Xunit;

    public class ValidationExtensionsTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("A", "a")]
        [InlineData("a", "a")]
        public void EqualIgnoreCase_MatchesCorrectly(string instance, string value)
        {
            var validator = new TestValidator();
            validator.RuleFor(x => x).EqualIgnoreCase(x => value).WithName("test");
            var result = validator.Validate(instance);

            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Theory]
        [InlineData("A", "b")]
        [InlineData("A", "B")]
        public void EqualIgnoreCase_FailforNonMatch(string instance, string value)
        {
            var validator = new TestValidator();
            validator.RuleFor(x => x).EqualIgnoreCase(x => value).WithName("test");
            var result = validator.Validate(instance);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle();
        }

        public class TestValidator : AbstractValidator<string> { }
    }
}
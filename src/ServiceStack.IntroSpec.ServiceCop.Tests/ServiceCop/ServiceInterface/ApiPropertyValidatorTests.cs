// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 
namespace ServiceStack.IntroSpec.ServiceCop.Tests.ServiceCop.ServiceInterface
{
    using FluentAssertions;
    using Models;
    using ServiceStack.IntroSpec.ServiceCop.ServiceInterface;
    using Xunit;

    public class ApiPropertyValidatorTests
    {
        private readonly ApiPropertyValidator validator;

        public ApiPropertyValidatorTests()
        {
            validator = new ApiPropertyValidator();
        }

        [Fact]
        public void ApiPropertyValidator_IdMustMatch()
        {
            var compare = GetValidCompare();
            compare.Original.Id = "aa";

            var result = validator.Validate(compare);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().Which.ErrorCode.Should().Be("PropertyIdChanged");
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ApiPropertyValidator_AllowMultiple_AllowedChangeIfNull(bool allowMultiple)
        {
            var compare = GetValidCompare();
            compare.Instance.AllowMultiple = allowMultiple;

            validator.Validate(compare).IsValid.Should().BeTrue();
        }

        [Fact]
        public void ApiPropertyValidator_AllowMultiple_AllowedChangeIfChangedFalseToTrue()
        {
            var compare = GetValidCompare();
            compare.Original.AllowMultiple = false;
            compare.Instance.AllowMultiple = true;

            validator.Validate(compare).IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(null)]
        public void ApiPropertyValidator_AllowMultiple_NotAllowedChangeIfChangedTrueToFalseOrNull(bool? allowMulitple)
        {
            var compare = GetValidCompare();
            compare.Original.AllowMultiple = true;
            compare.Instance.AllowMultiple = allowMulitple;

            var result = validator.Validate(compare);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().Which.ErrorCode.Should().Be("AllowMultipleChanged");
        }

        [Theory]
        [InlineData(null)]
        [InlineData(false)]
        public void ApiPropertyValidator_IsRequired_TrueChangedToNullOrFalse(bool isRequired)
        {
            var compare = GetValidCompare();
            compare.Original.IsRequired = true;
            compare.Instance.IsRequired = isRequired;

            var result = validator.Validate(compare);
            
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().Which.ErrorCode.Should().Be("IsRequiredChanged");
        }

        [Theory]
        [InlineData(null)]
        [InlineData(false)]
        public void ApiPropertyValidator_IsRequired_NullOrFalseChangedToTrue(bool isRequired)
        {
            var compare = GetValidCompare();
            compare.Original.IsRequired = isRequired;
            compare.Instance.IsRequired = true;

            var result = validator.Validate(compare);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().Which.ErrorCode.Should().Be("IsRequiredChanged");
        }

        [Fact]
        public void ApiPropertyValidator_IsRequired_FalseChangedToNull()
        {
            var compare = GetValidCompare();
            compare.Original.IsRequired = false;
            compare.Instance.IsRequired = null;

            validator.Validate(compare).IsValid.Should().BeTrue();
        }

        private static ApiPropertyCompare GetValidCompare()
        {
            var oldProp = new ApiPropertyDocumention { Id = "a" };
            var newProp = new ApiPropertyDocumention { Id = "a" };
            var compare = new ApiPropertyCompare { Original = oldProp, Instance = newProp };
            return compare;
        }
    }
}
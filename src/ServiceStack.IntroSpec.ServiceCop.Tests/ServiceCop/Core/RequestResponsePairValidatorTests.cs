// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 
namespace ServiceStack.IntroSpec.ServiceCop.Tests.ServiceCop.Core
{
    using FluentAssertions;
    using ServiceStack.IntroSpec.Models;
    using ServiceStack.IntroSpec.ServiceCop.Core;
    using Xunit;

    public class RequestResponsePairValidatorTests
    {
        [Fact]
        public void Returns_Error_For_NonMatching_ResponseName()
        {
            var validator = new RequestResponsePairValidator("req","res");
            var result = validator.Validate(new ApiResourceDocumentation
            {
                TypeName = "Blah",
                ReturnType = new ApiResourceType() { TypeName = "Bleh" }
            });

            result.IsValid.Should().BeFalse();
        }
    }
}
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 
namespace ServiceStack.IntroSpec.ServiceCop.Tests.ServiceCop.ServiceInterface
{
    using System;
    using ServiceStack.FluentValidation;
    using ServiceStack.FluentValidation.TestHelper;
    using ServiceStack.IntroSpec.ServiceCop.ServiceInterface;
    using ServiceStack.IntroSpec.ServiceCop.ServiceModel;

    using Xunit;

    public class ValidateServiceRequestValidatorTests
    {
        private readonly ValidateServiceRequestValidator validator;

        public ValidateServiceRequestValidatorTests()
        {
            validator = new ValidateServiceRequestValidator();
        }

        [Fact]
        public void ServiceIdIsRequiredIfNoUrlOrJson()
        {
            var model = new ValidateServiceRequest();
            validator.ShouldHaveValidationErrorFor(x => x.ServiceId, model);
        }

        [Fact]
        public void ServiceIdIsRequired()
        {
            var model = new ValidateServiceRequest { ServiceId = "test" };
            validator.ValidateAndThrow(model);
        }

        [Fact]
        public void ServiceUrlIsRequired()
        {
            var model = new ValidateServiceRequest { ServiceUrl = new Uri("http://a") };
            validator.ValidateAndThrow(model);
        }

        [Fact]
        public void ServiceUrlMustBeValidUrl()
        {
            var model = new ValidateServiceRequest { ServiceUrl = new Uri("a", UriKind.Relative) };
            validator.ShouldHaveValidationErrorFor(x => x.ServiceUrl, model);
        }

        [Fact]
        public void JsonIsRequired()
        {
            var model = new ValidateServiceRequest { IntroSpecJson = "{}" };
            validator.ValidateAndThrow(model);
        }
    }
}
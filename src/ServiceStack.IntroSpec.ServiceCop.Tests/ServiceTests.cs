// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.Tests
{
    using System;
    using FakeItEasy;
    using FluentAssertions;
    using ServiceStack.IntroSpec.ServiceCop.ServiceInterface;
    using ServiceStack.IntroSpec.ServiceCop.ServiceModel;
    using ServiceStack.Testing;
    using Xunit;

    public class ServiceTests : IDisposable
    {
        private readonly ServiceStackHost appHost;
        private readonly IServiceProvider<ServiceDetail> serviceProvider;

        public ServiceTests()
        {
            serviceProvider = A.Fake<IServiceProvider<ServiceDetail>>();

            appHost = new BasicAppHost(typeof(ServiceCopService).Assembly)
            {
                ConfigureContainer = container =>
                {
                    // Add your IoC dependencies here
                    container.Register(serviceProvider);
                }
            }
            .Init();
        }

        public void Dispose()
        {
            appHost.Dispose();
        }

        [Fact]
        public void TestGettingServiceDiscoveryResponse()
        {
            A.CallTo(() => serviceProvider.GetServices()).Returns(new[] { new ServiceDetail { Name = "bob" } });
            var service = appHost.Container.Resolve<ServiceCopService>();

            var response = (GetDiscoveryServicesResponse)service.Any(new GetDiscoveryServicesRequest());

            response.Services.Should().ContainSingle().Which.Name.Should().Be("bob");
        }
    }
}

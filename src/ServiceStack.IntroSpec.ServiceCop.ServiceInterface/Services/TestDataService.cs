﻿namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using System;
    using ServiceStack.Discovery.Consul;

    /// <summary>
    /// Test service only to create a couple of service registrations in consul
    /// </summary>
    public class TestDataService : IDisposable
    {
        public TestDataService()
        {
            ConsulClient.RegisterService(new ServiceRegistration
            {
                Id = "testid1",
                Name = "test1",
                Address = "http://127.0.0.1"
            });
            ConsulClient.RegisterService(new ServiceRegistration
            {
                Id = "testid2",
                Name = "test2",
                Address = "http://127.0.0.1"
            });
        }

        public void Dispose()
        {
            ConsulClient.UnregisterService("testId1");
            ConsulClient.UnregisterService("testId2");
        }
    }
}
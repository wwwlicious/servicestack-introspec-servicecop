// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 
namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    /// <summary>
    /// Handles interacting with the global service catalog
    /// </summary>
    /// <typeparam name="T">The dto type for a service registration</typeparam>
    public interface IServiceProvider<out T>
    {
        /// <summary>
        /// Gets all services
        /// </summary>
        /// <returns>Returns all the service DTOs</returns>
        T[] GetServices();

        /// <summary>
        /// Returns a specific service DTO
        /// </summary>
        /// <param name="serviceId">The service identifier</param>
        /// <returns>The service DTO</returns>
        T GetService(string serviceId);

        /// <summary>
        /// Suspends a service from the global catalog
        /// </summary>
        /// <param name="serviceId">The service identifier</param>
        /// <param name="maintenanceMessage">The reason for the service suspension</param>
        void SuspendService(string serviceId, string maintenanceMessage);

        /// <summary>
        /// Enables a service in the global catalog
        /// </summary>
        /// <param name="serviceId">The service identifier</param>
        void EnableService(string serviceId);
    }
}
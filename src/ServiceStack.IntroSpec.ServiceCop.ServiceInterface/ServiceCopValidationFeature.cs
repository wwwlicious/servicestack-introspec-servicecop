// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using System.Linq;
    using ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules;

    public class ServiceCopValidationFeature : IPlugin
    {
        private readonly RuleConfig config;

        /// <summary>
        /// Registered the servicecop validation feature
        /// </summary>
        /// <param name="config">The rule config to use, loads from introspec.json or default if no file found</param>
        public ServiceCopValidationFeature(RuleConfig config = null)
        {
            this.config = config == null ? RuleConfig.Load() : config.ThrowIfNull(nameof(config));
        }

        public void Register(IAppHost appHost)
        {
            appHost.Register(config);
        }
    }
}
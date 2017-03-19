// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using ServiceStack.Logging;

    public class ServiceCopValidationFeature : IPlugin
    {
        private readonly RuleConfig config;
        private readonly IStructuredLog logger = LogManager.LogFactory.GetStructuredLog();

        /// <summary>
        /// Registered the servicecop validation feature
        /// </summary>
        /// <param name="config">The rule config to use, loads from introspec.json or default if no file found</param>
        public ServiceCopValidationFeature(RuleConfig config = null)
        {
            logger.Debug("Loading ServiceCop configuration");
            this.config = config == null ? RuleConfig.Load() : config.ThrowIfNull(nameof(config));
        }

        public void Register(IAppHost appHost)
        {
            appHost.Register(config);
        }
    }
}
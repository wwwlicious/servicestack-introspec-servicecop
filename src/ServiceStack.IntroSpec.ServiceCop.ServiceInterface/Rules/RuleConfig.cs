// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using ServiceStack.Logging;

    public class RuleConfig
    {
        private const string ConfigFileName = "introspec.json";

        private static readonly ILog logger = LogManager.GetLogger(typeof(RuleConfig));

        protected RuleConfig()
        {
            // load config via Load()
        }

        public DtoRequestPostfixRule DtoRequestPostfixRule { get; set; } = new DtoRequestPostfixRule();

        public DtoRequestPrefixRule DtoRequestPrefixRule { get; set; } = new DtoRequestPrefixRule();

        public PluginRule PluginRule { get; set; } = new PluginRule();

        [IgnoreDataMember]
        public AbstractRule[] Rules
        {
            get
            {
                return
                    GetType()
                        .GetPublicProperties()
                        .Where(x => x.PropertyType.IsInstanceOf(typeof(AbstractRule)))
                        .Select(x => x.GetValue(this))
                        .Cast<AbstractRule>()
                        .Where(x => x.Enabled).ToArray();
            }
        }

        /// <summary>
        /// Loads rule config from the default config file name if found
        /// </summary>
        /// <returns></returns>
        public static RuleConfig Load()
        {
            RuleConfig result = null;
            try
            {
                result = File.ReadAllText(ConfigFileName).FromJson<RuleConfig>();
            }
            catch (FileNotFoundException ex)
            {
                logger.Warn($"{ex.FileName} was not found, loading defaults");
            }
            catch (Exception ex)
            {
                logger.Error($"Error loading introspec config from {ConfigFileName}, file appears invalid");
            }
            var config = result ?? new RuleConfig();
            
            // validators need to be created after the rule values are set
            foreach (var rule in config.Rules.Where(x => x.Enabled))
            {
                rule.CreateValidator();
            }
            return config;
        }

        /// <summary>
        /// Saves rule config to the default file
        /// </summary>
        public void Save()
        {
            File.WriteAllText(ConfigFileName, this.ToJson());
        }
    }
}
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using ServiceStack.Logging;
    using ServiceStack.Text;

    public class RuleConfig
    {
        private const string ConfigFileName = "introspec.json";

        private static readonly IStructuredLog logger = LogManager.LogFactory.GetStructuredLog();

        protected RuleConfig()
        {
            // load config via Load()
        }

        public DtoRequestPostfixRule DtoRequestPostfixRule { get; set; } = new DtoRequestPostfixRule();
        public DtoRequestPrefixRule DtoRequestPrefixRule { get; set; } = new DtoRequestPrefixRule();
        public RequestDocumentationRule RequestDocumentationRule { get; set; } = new RequestDocumentationRule();
        public PluginRule PluginRule { get; set; } = new PluginRule();
        public ApiActionRule ApiActionRule { get; set; } = new ApiActionRule();
        public NativeTypeRule NativeTypeRule { get; set; } = new NativeTypeRule();

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
                        .Where(x => x != null && x.Enabled).ToArray();
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
                logger.Warn(ex, "{FileName} was not found, loading defaults", ex.FileName);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error loading introspec config from {FileName}, file appears invalid", ConfigFileName);
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
            File.WriteAllText(ConfigFileName, this.ToJson().IndentJson());
        }
    }
}
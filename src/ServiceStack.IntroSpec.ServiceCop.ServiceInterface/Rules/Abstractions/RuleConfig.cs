// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using ServiceStack.FluentValidation;
    using ServiceStack.Logging;
    using ServiceStack.Text;

    public class RuleConfig
    {
        private const string ConfigFileName = "introspec.json";

        private static readonly IStructuredLog logger = LogManager.LogFactory.GetStructuredLog();
        private static List<IValidator> validators = new List<IValidator>();

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
        public RequestNameLengthRule RequestNameLengthRule { get; set; } = new RequestNameLengthRule();
        public RequestResponsePairRule RequestResponsePairRule { get; set; } = new RequestResponsePairRule();
        public ResponseEnumerableRule ResponseEnumerableRule { get; set; } = new ResponseEnumerableRule();
        public ResponseUnboundedResultsRule ResponseUnboundedResultsRule { get; set; } = new ResponseUnboundedResultsRule();
        public EnforceRequestValidationRule EnforceRequestValidationRule { get; set; } = new EnforceRequestValidationRule();

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

        [IgnoreDataMember]
        public IValidator[] Validators => validators.ToArray();

        /// <summary>
        /// Loads rule config from the default config file name if found
        /// </summary>
        /// <returns></returns>
        public static RuleConfig Load()
        {
            RuleConfig result = null;
            logger.Debug("Loading servicecop config from {ConfigFile}", ConfigFileName);
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
            foreach (var rule in config.Rules)
            {
                validators.Add(rule.CreateValidator());
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
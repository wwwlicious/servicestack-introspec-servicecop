// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using ServiceStack.FluentValidation;

    public class PluginRule : AbstractRule
    {
        public PluginRule()
        {
            Category = RuleCategories.Contract;
        }

        public MinimumPluginVersion[] MinimumPluginVersions { get; set; }

        public override IValidator CreateValidator()
        {
            return new PluginValidator(this);
        }

        public class MinimumPluginVersion
        {
            public string PluginName { get; set; }
            public int Version { get; set; }
        }
    }
}
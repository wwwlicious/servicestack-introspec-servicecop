// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules
{
    using System.Runtime.Serialization;
    using ServiceStack.FluentValidation;

    /// <summary>
    /// The base class for all rules, used to serialise the rule config <see cref="RuleConfig"/>
    /// </summary>
    public abstract class AbstractRule : IRule, IRuleConfig
    {
        public string Id { get; protected set; }

        // TODO enabled only true by default for dev, turn off before releasing
        public bool Enabled { get; set; } = true;

        public Severity Severity { get; set; } = Severity.Error;

        [IgnoreDataMember]
        public string Category { get; protected set; }
        [IgnoreDataMember]
        public IValidator Validator { get; set; }

        public abstract void CreateValidator();
    }
}
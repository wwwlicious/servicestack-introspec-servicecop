// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using ServiceStack.FluentValidation;

    /// <summary>
    /// Interface to hold the config for a rule
    /// </summary>
    public interface IRuleConfig
    {
        bool Enabled { get; set; }
        Severity Severity { set; get; }
    }
}
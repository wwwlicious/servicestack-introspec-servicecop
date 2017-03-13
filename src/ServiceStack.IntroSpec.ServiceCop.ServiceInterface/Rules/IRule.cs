// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules
{
    using ServiceStack.FluentValidation;

    /// <summary>
    /// interface to hold the category, test and validator for an api rule
    /// </summary>
    public interface IRule
    {
        string Category { get; }
        IValidator Validator { get; set; }
    }
}
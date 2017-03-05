// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.ServiceModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ServiceStack.FluentValidation;
    using ServiceStack.FluentValidation.Results;

    public class ValidateServiceResponse
    {
        public SpecValidationResult Result { get; set; }
    }

    /// <summary>
    /// Required due to bug in introspec reflectionenricher as ValidationResult contains circular type reference between IRequest and IResponse
    /// </summary>
    public class SpecValidationResult
    {
        private readonly List<SpecValidationFailure> errors = new List<SpecValidationFailure>();

        public SpecValidationResult()
        {
        }

        public SpecValidationResult(ValidationResult failures)
        {
            errors.AddRange(failures.Errors.Select(x => new SpecValidationFailure().PopulateWith(x)));
        }

        public bool IsValid => errors.Count == 0;

        public IList<SpecValidationFailure> Errors => errors;
    }

    [Serializable]
    public class SpecValidationFailure
    {
        /// <summary>The name of the property.</summary>
        public string PropertyName { get; set; }

        /// <summary>The error message</summary>
        public string ErrorMessage { get; set; }

        /// <summary>The error code</summary>
        public string ErrorCode { get; set; }

        /// <summary>The property value that caused the failure.</summary>
        public object AttemptedValue { get; set; }

        /// <summary>Custom state associated with the failure.</summary>
        public object CustomState { get; set; }

        public Severity Severity { get; set; }

        /// <summary>
        /// Placeholder values used for string substitution when building ErrorMessage
        /// </summary>
        public Dictionary<string, string> PlaceholderValues { get; set; }
    }
}
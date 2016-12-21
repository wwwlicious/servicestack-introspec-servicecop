namespace ServiceStack.IntroSpec.ServiceCop.ServiceModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ValidateServiceResponse
    {
        public SpecValidationResult Result { get; set; }
    }

    public class SpecValidationResult
    {
        private readonly List<SpecValidationFailure> errors = new List<SpecValidationFailure>();

        public SpecValidationResult()
        {
        }

        public SpecValidationResult(IEnumerable<SpecValidationFailure> failures)
        {
            this.errors.AddRange(failures.Where(failure => failure != null));
        }

        public bool IsValid => Errors.Count == 0;

        public IList<SpecValidationFailure> Errors => (errors as IList<SpecValidationFailure>);
    }

    [Serializable]
    public class SpecValidationFailure
    {
        /// <summary>The name of the property.</summary>
        public string PropertyName { get; private set; }

        /// <summary>The error message</summary>
        public string ErrorMessage { get; private set; }

        /// <summary>The error code</summary>
        public string ErrorCode { get; private set; }

        /// <summary>The property value that caused the failure.</summary>
        public object AttemptedValue { get; private set; }

        /// <summary>Custom state associated with the failure.</summary>
        public object CustomState { get; set; }

        /// <summary>
        /// Placeholder values used for string substitution when building ErrorMessage
        /// </summary>
        public Dictionary<string, string> PlaceholderValues { get; set; }

        /// <summary>Creates a new ValidationFailure.</summary>
        public SpecValidationFailure(string propertyName, string error, string errorCode, object attemptedValue = null)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = error;
            this.AttemptedValue = attemptedValue;
            this.ErrorCode = errorCode;
        }

        /// <summary>Creates a textual representation of the failure.</summary>
        public override string ToString()
        {
            return this.ErrorMessage;
        }
    }
}
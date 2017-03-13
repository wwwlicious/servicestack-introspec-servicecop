// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.FluentValidation;

    public class SnapshotApiPropertyValidator : AbstractValidator<ApiPropertyCompare>
    {
        /// <summary>
        /// TODO These should be separate rules
        /// </summary>
        public SnapshotApiPropertyValidator()
        {
            RuleFor(x => x.Instance.Id)
                .EqualIgnoreCase(x => x.Original.Id)
                .WithName("Id")
                .WithMessage("{PropertyName} '{PropertyValue}' does not match")
                .WithErrorCode(SnapshotErrorCodes.PropertyIdChanged);

            RuleFor(x => x.Instance.AllowMultiple)
                .Must((x, instance) => AllowMultipleValidate(instance, x.Original.AllowMultiple))
                .WithName("AllowMultiple")
                .WithMessage("{PropertyName} '{PropertyValue}' has a breaking change")
                .WithErrorCode(SnapshotErrorCodes.AllowMultipleChanged);

            RuleFor(x => x.Instance.IsRequired)
                .Must((x, instance) => IsRequiredValidate(instance, x.Original.IsRequired))
                .WithName("IsRequired")
                .WithMessage("{PropertyName} has been enabled")
                .WithErrorCode(SnapshotErrorCodes.IsRequiredChanged);

            // TODO write tests
            RuleFor(x => x.Instance.ClrType)
                .Equal(x => x.Original.ClrType)
                .WithName("ClrType")
                .WithMessage("{PropertyName} '{PropertyValue} has changed")
                .WithErrorCode(SnapshotErrorCodes.ClrTypeChanged);

            // Create constraint validator
            // RuleFor(x => x.Instance.Contraints).SetValidator()
            
            // Create EmbeddedResourceValidator
            // RuleFor(x => x.Instance.EmbeddedResource)

            RuleFor(x => x.Instance.ParamType)
                .Equal(x => x.Original.ParamType)
                .WithName("ParamType")
                .WithMessage("ParamType {PropertyValue} does not match {ComparisonValue}")
                .WithErrorCode(SnapshotErrorCodes.ParamTypeChanged);
        }

        private bool IsRequiredValidate(bool? instance, bool? original)
        {
            // a breaking change happens when:
            // null | false : true
            // true | null
            if ((!original.HasValue || original.Value == false) && instance.HasValue && instance.Value) return false;
            return original.HasValue && !instance.HasValue;
        }

        private bool AllowMultipleValidate(bool? instance, bool? original)
        {
            // a breaking change happens when:
            // original has a value and that value is changed from allowing -> not allowing multiple
            if (!original.HasValue) return true;
            if (!instance.HasValue) return false;
            return !original.Value || instance.Value;
        }
    }
}
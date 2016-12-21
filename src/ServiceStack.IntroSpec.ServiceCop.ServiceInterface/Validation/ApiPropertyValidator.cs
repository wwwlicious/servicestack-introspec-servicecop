namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    public class ApiPropertyValidator : AbstractValidator<ApiPropertyCompare>
    {
        public ApiPropertyValidator()
        {
            RuleFor(x => x.Instance.Id)
                .EqualIgnoreCase(x => x.Original.Id)
                .WithName("Id")
                .WithMessage("{PropertyName} '{PropertyValue}' does not match")
                .WithErrorCode(ApiCompareErrorCodes.PropertyIdChanged);

            RuleFor(x => x.Instance.AllowMultiple)
                .Must((x, instance) => AllowMultipleValidate(instance, x.Original.AllowMultiple))
                .WithName("AllowMultiple")
                .WithMessage("{PropertyName} '{PropertyValue}' has a breaking change")
                .WithErrorCode(ApiCompareErrorCodes.AllowMulitpleChanged);

            RuleFor(x => x.Instance.IsRequired)
                .Must((x, instance) => IsRequiredValidate(instance, x.Original.IsRequired))
                .WithName("IsRequired")
                .WithMessage("{PropertyName} has been enabled")
                .WithErrorCode(ApiCompareErrorCodes.IsRequiredChanged);

            // TODO write tests
            RuleFor(x => x.Instance.ClrType)
                .Equal(x => x.Original.ClrType)
                .WithName("ClrType")
                .WithMessage("{PropertyName} '{PropertyValue} has changed")
                .WithErrorCode(ApiCompareErrorCodes.ClrTypeChanged);

            // Create constraint validator
            //RuleFor(x => x.Instance.Contraints).SetValidator()
            
            // Create EmbeddedResourceValidator
            //RuleFor(x => x.Instance.EmbeddedResource)

            RuleFor(x => x.Instance.ParamType)
                .Equal(x => x.Original.ParamType)
                .WithName("ParamType")
                .WithMessage("ParamType {PropertyValue} does not match {ComparisonValue}")
                .WithErrorCode(ApiCompareErrorCodes.ParamTypeChanged);
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
            // original has a value and value changed from allowing -> not allowing mutiple
            if (!original.HasValue) return true;
            if (!instance.HasValue) return false;
            return !original.Value || instance.Value;
        }
    }

    public class ApiPropertyCompare
    {
        public ApiPropertyDocumention Original { get; set; }
        public ApiPropertyDocumention Instance { get; set; }
    }
}
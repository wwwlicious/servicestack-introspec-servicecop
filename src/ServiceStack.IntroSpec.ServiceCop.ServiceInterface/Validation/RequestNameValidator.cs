namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using System.Net.NetworkInformation;
    using ServiceStack.Configuration;
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    //public class RequestNameValidator : AbstractValidator<ApiResourceDocumentation>
    //{
    //    public RequestNameValidator()
    //    {
    //        RuleFor(x => x.TypeName).Must(x => x.EndsWith("Request"));
    //    }
    //}

    public interface IServiceCopRule
    {
        string RuleId { get; }
        string ServiceName { get; }
        string RequestName { get; }
        Severity RuleSeverity { get; }
    }

    public class ServiceCopRule<T> : IServiceCopRule
    {
        private readonly string ruleId;

        public ServiceCopRule(string ruleId, string serviceName, Severity severity = Severity.Error)
        {
            ServiceName = serviceName;
            Severity = severity;
            this.ruleId = ruleId;
            RequestName = typeof(T).Name;
        }
        public string RuleId { get; private set; }
        public string ServiceName { get; private set; }
        public Severity Severity { get; set; }
        public string RequestName { get; private set; }
        public Severity RuleSeverity { get; private set; }
    }

    public abstract class ServiceCopValidator<T> : AbstractValidator<T>, IServiceCopRule
    {
        private readonly IServiceCopRule serviceCopRuleImplementation;

        protected ServiceCopValidator(IServiceCopRule serviceCopRuleImplementation)
        {
            this.serviceCopRuleImplementation = serviceCopRuleImplementation;
        }

        public string RuleId => serviceCopRuleImplementation.RuleId;

        public string ServiceName => serviceCopRuleImplementation.ServiceName;

        public string RequestName => serviceCopRuleImplementation.RequestName;

        public Severity RuleSeverity => serviceCopRuleImplementation.RuleSeverity;
    }

    public class RequestNameValidator : ServiceCopValidator<ApiResourceDocumentation>
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }

        public RequestNameValidator() : base(new ServiceCopRule<ApiDocumentation>("RequestName", "blah"))
        {
            RuleFor(x => x.TypeName)
                .Must(x => x.StartsWith(Prefix))
                .WithSeverity(RuleSeverity)
                .WithErrorCode("InvalidRequestPrefix")
                .Unless(x => Prefix.IsNullOrEmpty());

            RuleFor(x => x.TypeName)
                .Must(x => x.EndsWith(Suffix))
                .WithSeverity(RuleSeverity)
                .WithErrorCode("InvalidRequestSuffix")
                .Unless(x => Suffix.IsNullOrEmpty());
        }
    }
}
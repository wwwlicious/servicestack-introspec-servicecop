namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.ServiceCop.ServiceModel;

    public class ValidateServiceRequestValidator : AbstractValidator<ValidateServiceRequest>
    {
        public ValidateServiceRequestValidator()
        {
            // order of precedence id > url > json 
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.ServiceId).NotEmpty().Unless(x => x.ServiceUrl.IsWellFormedOriginalString() || x.IntroSpecJson != null);
            RuleFor(x => x.ServiceUrl).NotEmpty().Unless(x => x.IntroSpecJson != null);
            RuleFor(x => x.IntroSpecJson).NotEmpty();
        }
    }
}
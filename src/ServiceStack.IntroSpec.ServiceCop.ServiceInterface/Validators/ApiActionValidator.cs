namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    public class ApiActionValidator : AbstractValidator<ApiAction>
    {
        public ApiActionValidator()
        {
            // TODO Will need to add config, ability to add exceptions etc
            RuleFor(x => x.Security)
                .Must(x => x.IsProtected)
                .WithMessage("Every request must be protected from anonymous callers");
        }
    }
}
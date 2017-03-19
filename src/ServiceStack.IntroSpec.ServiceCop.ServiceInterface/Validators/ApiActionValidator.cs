namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    public class ApiActionsValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public ApiActionsValidator()
        {
            RuleFor(x => x.Actions).SetCollectionValidator(new ApiActionValidator());
        }
    }

    public class ApiActionValidator : AbstractValidator<ApiAction>
    {
        public ApiActionValidator()
        {
            // TODO Will need to add config, ability to add exceptions etc
            RuleFor(x => x.Security)
                .Must(x => x.IsProtected)
                .WithMessage("{0} is not protected from anonymous callers", action => action.Verb);
        }
    }
}
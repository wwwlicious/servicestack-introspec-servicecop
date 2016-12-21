namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.FluentValidation;

    public class StringTokenValidator : AbstractValidator<string>
    {
        public StringTokenValidator()
        {
            // should tokenize caps into words
            // single word requests should fail (and produce service warnings)
            // why?
            // because it is likely that it would clash or not be meaningful
            // amoungst hundreds of other services
            // ex. OrderRequest could end up in purchase orders, sales orders, works orders etc
            // need specific names
        }
    }
}
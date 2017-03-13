namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using System.Linq;
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    /// <summary>
    /// return type containing an enumerable prop should also have skip/take props to restrict unbounded result sets 
    /// Why?
    /// Returning collection properties without also including some paging semantics means that endpoints are potentially
    /// returning an unlimited number of results which will create unpredicable endpoint response times and load.
    /// TODO make semantic naming configurable (prev,next,page,pageindex) from rule config
    /// TODO allow DTO specific admin overrides/exceptions to this rule
    /// </summary>
    public class ResponseUnboundedResultsetValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public ResponseUnboundedResultsetValidator()
        {
            // checks for an numeric property named skip if any property is a collection type
            RuleFor(x => x.ReturnType.Properties)
                .Must(x => x.Any(prop => StringExtensions.EqualsIgnoreCase(prop.Title, "Skip") && ReflectionExtensions.IsNumericType(prop.ClrType)))
                .When(x => x.ReturnType.Properties.Any(prop => prop.IsCollection.Value == true));

            // checks for an numeric property named skip if any property is a collection type
            RuleFor(x => x.ReturnType.Properties)
                .Must(x => x.Any(prop => prop.Title.EqualsIgnoreCase("Take") && prop.ClrType.IsNumericType()))
                .When(x => x.ReturnType.Properties.Any(prop => prop.IsCollection.Value == true));
        }
    }
}
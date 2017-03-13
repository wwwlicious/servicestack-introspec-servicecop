namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    /// <summary>
    /// return types should not collection types (IEnumerable, Arrays, Lists)
    /// Why?
    /// A strong indicator of returning unbounded (non-paged) result sets which
    /// can lead to unpredicable response times and load for endpoints.
    /// Also returning raw collections will allows the items to have addative contract changes
    /// but not addative contract changes to the response DTO such as information outwith the 
    /// collection such as paging, total counts etc
    /// </summary>
    public class ResponseEnumerableValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public ResponseEnumerableValidator()
        {
            RuleFor(x => x.ReturnType.IsCollection).Must(r => r.Value == false);
        }
    }
}
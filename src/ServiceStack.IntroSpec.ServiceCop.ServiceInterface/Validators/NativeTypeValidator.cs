namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    /// <summary>
    /// type cannot be system type (string, int, bool)
    /// </summary>
    /// <remarks>
    /// Why? 
    /// Accepting native types (strings, ints, bools etc) creates a weak contract that you cannot make addative changes to.
    /// The same applies for framework types, if you are using your own DTO, you cannot extend it, without breaking the contract 
    /// This rule is designed to check and enforce the use of POCO types (Applicable to BOTH request and responses
    /// </remarks>
    public class NativeTypeValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public NativeTypeValidator()
        {
            // Add CLR type string to ApiResourceDocumentation (DTO) and Response to make it easier to
            // check for native types?
            RuleFor(x => x.TypeName).Configure(rule =>
            {
                // myName.GetType().Module.ScopeName == "CommonLanguageRuntimeLibrary"
            });
        }
    }
}
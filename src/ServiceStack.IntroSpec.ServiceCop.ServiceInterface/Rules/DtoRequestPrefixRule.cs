namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules
{
    public class DtoRequestPrefixRule : AbstractRule
    {
        public DtoRequestPrefixRule()
        {
            Id = RuleIds.DtoRequestPrefix;
            Category = "Naming";
        }

        public override void CreateValidator()
        {
            Validator = new DtoRequestPrefixValidator(Severity);
        }
    }
}
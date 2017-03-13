namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules
{
    using ServiceStack.FluentValidation;

    public class PluginRule : AbstractRule
    {
        public string Category { get; }

        public IValidator Validator { get; set; }

        public MinimumPluginVersion[] MinimumPluginVersions { get; set; }

        public override void CreateValidator()
        {
            //Validator = new PluginValidator(MinimumPluginVersions);
        }

        public class MinimumPluginVersion
        {
            public string PluginName { get; set; }
            public int Version { get; set; }
        }
    }
}
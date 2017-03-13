namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class StringExtensions
    {
        public static bool StartsWithAny(this string value, IEnumerable<string> values, StringComparison comparison)
        {
            return values.Any(v => value.StartsWith(v, comparison));
        }
    }
}
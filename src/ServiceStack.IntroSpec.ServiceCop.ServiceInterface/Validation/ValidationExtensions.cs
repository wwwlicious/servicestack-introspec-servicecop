// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using ServiceStack.FluentValidation;
    using ServiceStack.FluentValidation.Internal;
    using ServiceStack.FluentValidation.Validators;

    public static class ValidationExtensions
    {
        private const string SemVerRegEx =
            "/(?<=^[Vv]|^)(?:(?<major>(?:0|[1-9](?:(?:0|[1-9])+)*))[.](?<minor>(?:0|[1-9](?:(?:0|[1-9])+)*))[.](?<patch>(?:0|[1-9](?:(?:0|[1-9])+)*))(?:-(?<prerelease>(?:(?:(?:[A-Za-z]|-)(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)?|(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)(?:[A-Za-z]|-)(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)?)|(?:0|[1-9](?:(?:0|[1-9])+)*))(?:[.](?:(?:(?:[A-Za-z]|-)(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)?|(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)(?:[A-Za-z]|-)(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)?)|(?:0|[1-9](?:(?:0|[1-9])+)*)))*))?(?:[+](?<build>(?:(?:(?:[A-Za-z]|-)(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)?|(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)(?:[A-Za-z]|-)(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)?)|(?:(?:0|[1-9])+))(?:[.](?:(?:(?:[A-Za-z]|-)(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)?|(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)(?:[A-Za-z]|-)(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)?)|(?:(?:0|[1-9])+)))*))?)$/";

        /// <summary>
        /// Checks that a property has a valid semver string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> SemVer<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new RegularExpressionValidator(SemVerRegEx));
        }

        /// <summary>
        /// Checks that a property is a well formed relative or absolute url
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="uriKind"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> Url<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, UriKind uriKind)
        {
            return ruleBuilder.Must(x => Uri.IsWellFormedUriString(x.ToString(), uriKind));
        }

        /// <summary>
        /// Checks string equality ignoring case
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> EqualIgnoreCase<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Expression<Func<T, string>> expression) 
        {
            var func = expression.Compile();
            return ruleBuilder.SetValidator(new EqualValidator(func.CoerceToNonGeneric(), expression.GetMember(), StringComparer.OrdinalIgnoreCase));
        }

        public static IRuleBuilderOptions<T, string> MinimumWords<T>(
            this IRuleBuilderOptions<T, string> ruleBuilder, int minimumWords)
        {
            return ruleBuilder.SetValidator(new MinimumLengthValidator(minimumWords));
        }

        public static IRuleBuilderOptions<T, IEnumerable<string>> NoDuplicates<T>(
            this IRuleBuilderOptions<T, IEnumerable<string>> ruleBuilder)
        {
            return ruleBuilder.Must(x =>
            {
                var items = x.ToArray();
                return items.Count() == items.Distinct().Count();
            });
        }
    }
}
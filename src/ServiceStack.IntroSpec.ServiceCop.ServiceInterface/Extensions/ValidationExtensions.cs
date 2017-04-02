// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Semver;
    using ServiceStack.FluentValidation;
    using ServiceStack.FluentValidation.Internal;
    using ServiceStack.FluentValidation.Validators;

    public static class ValidationExtensions
    {
        /// <summary>
        /// Checks that a property has a valid semver string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> SemVer<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x => SemVersion.TryParse(x, out var semver, true));
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

        /// <summary>
        /// Checks a string for a minimum number of words
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="minimumWords">the minimum number of words the string should contain</param>
        /// <param name="splitCamelCase">Will split any CamelCase words</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> MinimumWords<T>(
            this IRuleBuilder<T, string> ruleBuilder, int minimumWords, bool splitCamelCase = false)
        {
            return ruleBuilder.Must(x => splitCamelCase ? x.SplitCamelCase().Split().Length >= minimumWords : x.Split(' ').Length >= minimumWords)
                .WithErrorCode("MinimumWords")
                .WithMessage($"{{PropertyName}} must have at least {minimumWords} words");
        }

        /// <summary>
        /// Checks a string collection to ensure there are no duplicates
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, IEnumerable<string>> NoDuplicates<T>(
            this IRuleBuilder<T, IEnumerable<string>> ruleBuilder)
        {
            return ruleBuilder.Must(x => !x.Duplicates().Any())
                .WithMessage("The following duplicates were found: {PlaceHolderValues}", (value, enumerable) => enumerable.Duplicates());
        }
    }
}
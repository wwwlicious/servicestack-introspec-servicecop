﻿namespace ServiceStack.IntroSpec.ServiceCop.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using ServiceStack.FluentValidation;
    using ServiceStack.FluentValidation.Internal;
    using ServiceStack.FluentValidation.Validators;

    public static class ValidationExtensions
    {
        private const string SemVerRegEx =
            "/(?<=^[Vv]|^)(?:(?<major>(?:0|[1-9](?:(?:0|[1-9])+)*))[.](?<minor>(?:0|[1-9](?:(?:0|[1-9])+)*))[.](?<patch>(?:0|[1-9](?:(?:0|[1-9])+)*))(?:-(?<prerelease>(?:(?:(?:[A-Za-z]|-)(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)?|(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)(?:[A-Za-z]|-)(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)?)|(?:0|[1-9](?:(?:0|[1-9])+)*))(?:[.](?:(?:(?:[A-Za-z]|-)(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)?|(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)(?:[A-Za-z]|-)(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)?)|(?:0|[1-9](?:(?:0|[1-9])+)*)))*))?(?:[+](?<build>(?:(?:(?:[A-Za-z]|-)(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)?|(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)(?:[A-Za-z]|-)(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)?)|(?:(?:0|[1-9])+))(?:[.](?:(?:(?:[A-Za-z]|-)(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)?|(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)(?:[A-Za-z]|-)(?:(?:(?:0|[1-9])|(?:[A-Za-z]|-))+)?)|(?:(?:0|[1-9])+)))*))?)$/";

        public static IRuleBuilderOptions<T, TProperty> SemVer<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new RegularExpressionValidator(SemVerRegEx));
        }

        public static IRuleBuilderOptions<T, TProperty> Url<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.Must(x => Uri.IsWellFormedUriString(x.ToString(), UriKind.RelativeOrAbsolute));
        }

        public static IRuleBuilderOptions<T, TProperty> EqualIgnoreCase<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Expression<Func<T, string>> expression) 
        {
            var func = expression.Compile();
            return ruleBuilder.SetValidator(new EqualValidator(func.CoerceToNonGeneric(), expression.GetMember(), StringComparer.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Specifies custom severity that should be stored alongside the validation message when validation fails for this rule.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="rule"></param>
        /// <param name="severity"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> WithSeverity<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, Severity severity)
        {
            return rule.Configure(x => x.CurrentValidator.CustomStateProvider = o => severity as object);
            //return rule.Configure(config => config.CurrentValidator.Severity = severity);
        }
    }

    public enum Severity
    {
        Information,
        Warning,
        Error
    }
}
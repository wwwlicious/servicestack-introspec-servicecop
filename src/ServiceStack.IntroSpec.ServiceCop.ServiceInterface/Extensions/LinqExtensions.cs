// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 
namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ServiceStack.FluentValidation;

    public static class LinqExtensions
    {
        /// <summary>
        /// Method that returns all the duplicates (distinct) in the collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="source">The source collection to detect for duplicates</param>
        /// <param name="distinct">Specify <b>true</b> to only return distinct elements.</param>
        /// <returns>A distinct list of duplicates found in the source collection.</returns>
        /// <remarks>This is an extension method to IEnumerable&lt;T&gt;</remarks>
        public static IEnumerable<T> Duplicates<T>
                 (this IEnumerable<T> source, bool distinct = true)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            // select the elements that are repeated
            var result = source.GroupBy(a => a).SelectMany(a => a.Skip(1));

            // distinct?
            if (distinct)
            {
                // deferred execution helps us here
                result = result.Distinct();
            }

            return result;
        }

        public static IEnumerable<IValidator<TClass>> ForValidating<TClass>(this IEnumerable<IValidator> validators) where TClass : class 
        {
            return
                validators.Where(x => x.CanValidateInstancesOfType(typeof(TClass)))
                    .Cast<IValidator<TClass>>();
        }
    }
}
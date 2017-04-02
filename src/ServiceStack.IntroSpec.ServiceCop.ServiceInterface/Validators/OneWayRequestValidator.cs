// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 
namespace ServiceStack.IntroSpec.ServiceCop.Core
{
    using System.Linq;
    using System.Net;
    using ServiceStack.FluentValidation;
    using ServiceStack.IntroSpec.Models;

    public class OneWayRequestValidator : AbstractValidator<ApiResourceDocumentation>
    {
        public OneWayRequestValidator()
        {
            // one way requests need to include a 204 : no content in the action types
            When(x => x.ReturnType == null, () =>
            {
                RuleForEach(x => x.Actions).Must(x => x.StatusCodes.Any(s => s.Code.Equals(HttpStatusCode.NoContent)));
            });
        }    
    }
}
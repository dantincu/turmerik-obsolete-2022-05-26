using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Components;

namespace Turmerik.AspNetCore.Services
{
    public abstract class ApiControllerRelUriRetrieverBase<TController>
    {
        private readonly ILambdaExprHelper<TController> lambdaExprHelper;

        protected ApiControllerRelUriRetrieverBase(
            ILambdaExprHelperFactory lambdaExprHelperFactory)
        {
            lambdaExprHelper = lambdaExprHelperFactory.GetHelper<TController>();
        }

        protected abstract string ControllerBaseRelUri { get; }

        protected string GetControllerRelUri<TProperty>(Expression<Func<TController, TProperty>> controllerMethodExpr)
        {
            string methodName = lambdaExprHelper.MethodName(controllerMethodExpr);
            string relUri = string.Join("/", ControllerBaseRelUri, methodName);

            return relUri;
        }
    }
}

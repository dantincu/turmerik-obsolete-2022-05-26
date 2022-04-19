using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Turmerik.Core.Helpers
{
    public class LambdaH<TSource>
    {
        public PropertyInfo Prop<TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda,
            Type type = null, bool checkReflectedType = false)
        {
            type = type ?? typeof(TSource);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            ThrowErrIfReq(() => member == null, propertyLambda);

            PropertyInfo propInfo = member.Member as PropertyInfo;
            ThrowErrIfReq(() => propInfo == null, propertyLambda);

            ThrowErrIfReq(
                () => checkReflectedType && !propInfo.ReflectedType.IsAssignableFrom(type),
                propertyLambda,
                () => string.Format(
                    "Expression '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));

            return propInfo;
        }

        public string Name<TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda,
            Type type = null, bool checkReflectedType = false)
        {
            PropertyInfo propInfo = Prop(propertyLambda, type, checkReflectedType);
            string name = propInfo.Name;

            return name;
        }

        private static T ThrowErr<T, TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda,
            Func<string> messageFactory = null)
        {
            string message = messageFactory?.Invoke() ?? string.Format(
                "Expression '{0}' does not refer to a property.",
                propertyLambda.ToString());

            throw new ArgumentException(message);
        }

        private static void ThrowErrIfReq<TProperty>(
            Func<bool> condition,
            Expression<Func<TSource, TProperty>> propertyLambda,
            Func<string> messageFactory = null)
        {
            if (condition())
            {
                ThrowErr<object, TProperty>(
                    propertyLambda, messageFactory);
            }
        }
    }
}

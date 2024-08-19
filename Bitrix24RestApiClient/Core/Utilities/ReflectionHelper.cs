using System;
using System.Linq;
using Newtonsoft.Json;
using System.Reflection;
using System.Linq.Expressions;
using Bitrix24RestApiClient.Core.Attributes;

namespace Bitrix24RestApiClient.Core.Utilities;

public static class ReflectionHelper
{
    public static string GetPropertyNameFromJsonPropertyAttribute(MemberInfo memberInfo)
    {
        var propertyNameArgument = memberInfo.CustomAttributes
            .FirstOrDefault(x => x.AttributeType.FullName == typeof(JsonPropertyAttribute).FullName)
            ?.ConstructorArguments.FirstOrDefault();

        return propertyNameArgument?.Value as string;
    }

    public static string GetPropertyNameFromCrmFieldAttribute(MemberInfo memberInfo)
    {
        var propertyNameArgument = memberInfo.CustomAttributes
            .FirstOrDefault(x => x.AttributeType.FullName == typeof(CrmFieldAttribute).FullName)
            ?.ConstructorArguments.FirstOrDefault();

        return propertyNameArgument?.Value as string;
    }

    public static MemberInfo GetMemberInfo(LambdaExpression expression)
    {
        switch (expression.Body.NodeType)
        {
            case ExpressionType.Convert:
            {
                var body = (UnaryExpression)expression.Body;
                return ((MemberExpression)body.Operand).Member;
            }
            case ExpressionType.MemberAccess:
                return ((MemberExpression)expression.Body).Member;
            default:
                throw new ArgumentException("Not a member access", nameof(expression));
        }
    }

    public static object? GetPropertyValue<TEntity>(LambdaExpression expression, TEntity obj)
    {
        MemberExpression memberExpr = null;

        switch (expression.Body.NodeType)
        {
            case ExpressionType.Convert:
            {
                var body = (UnaryExpression)expression.Body;
                memberExpr = (MemberExpression)body.Operand;
                break;
            }
            case ExpressionType.MemberAccess:
                memberExpr = (MemberExpression)expression.Body;
                break;
        }

        if (memberExpr != null)
            return ((PropertyInfo)memberExpr.Member).GetValue(obj);

        throw new ArgumentException("Not a member access", nameof(expression));
    }
}
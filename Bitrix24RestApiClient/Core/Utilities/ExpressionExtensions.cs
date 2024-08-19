using System;
using System.Linq;
using System.Linq.Expressions;
using Bitrix24RestApiClient.Core.Attributes;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Api.Crm.Invoices.OldInvoices.Enums;

namespace Bitrix24RestApiClient.Core.Utilities;

public static class ExpressionExtensions
{
    public static string JsonPropertyNameByExpr<TEntity>(Expression<Func<TEntity, object>> expr)
    {
        var memberInfo = ReflectionHelper.GetMemberInfo(expr);

        return ReflectionHelper.GetPropertyNameFromJsonPropertyAttribute(memberInfo)
               ?? ReflectionHelper.GetPropertyNameFromCrmFieldAttribute(memberInfo)
               ?? memberInfo.Name;
    }

    public static string JsonPropertyName<TEntity>(this Expression<Func<TEntity, object>> self) => 
        JsonPropertyNameByExpr(self);

    public static object MapValue<TEntity>(this Expression<Func<TEntity, object>> self, object value)
    {
        var memberInfo = ReflectionHelper.GetMemberInfo(self);

        var crmField = memberInfo.CustomAttributes
            .FirstOrDefault(x => x.AttributeType.FullName == typeof(CrmFieldAttribute).FullName);

        if (crmField == null) return value;
        var fieldTypeObj = crmField.ConstructorArguments.ToArray()[1].Value;
        if (fieldTypeObj == null)
            throw new NullReferenceException();

        var fieldType = (CrmFieldSubTypeEnum)fieldTypeObj;
        switch (fieldType)
        {
            case CrmFieldSubTypeEnum.DateTimeWithFormatDdMmYyyy_HhMmSs:
                const string format = "dd.MM.yyyy HH:mm:ss";

                return value switch
                {
                    DateTimeOffset offset => offset.ToUniversalTime().ToString(format),
                    DateTime time => time.ToString(format),
                    _ => value
                };

            case CrmFieldSubTypeEnum.Char_YesNo:
                var boolValue = value as bool? ?? throw new NullReferenceException("Для свойства, которое принимает Y или N нул недопустим");

                return boolValue
                    ? YesNoEnum.Y.ToString("F")
                    : YesNoEnum.N.ToString("F");

            case CrmFieldSubTypeEnum.String_StatusSemanticIdEnum:
                var enumValue = (value as StatusSemanticIdEnum?) ?? throw new NullReferenceException("Для свойства, которое принимает F, S или P нул недопустим");

                return enumValue switch
                {
                    StatusSemanticIdEnum.Failed => "F" //TODO дубль строки
                    ,
                    StatusSemanticIdEnum.Success => "S",
                    StatusSemanticIdEnum.Processing => "P",
                    _ => throw new ArgumentOutOfRangeException()
                };

            case CrmFieldSubTypeEnum.String_InvoiceStatusEnum:
                return (value as InvoiceStatusEnum).StatusId;

            case CrmFieldSubTypeEnum.Int_EntityTypeIdEnum:
                return EntityTypeIdEnum.Create((int)value);

            case CrmFieldSubTypeEnum.Int:
                return (int?)value;

            default:
                throw new ArgumentOutOfRangeException();
        }

        return value;
    }
}
using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Core.Models.CrmTypes.CrmMultiField;

public class CrmMultiFieldEmail: CrmMultiField
{
    public CrmMultiFieldEmail() { }
    public CrmMultiFieldEmail(string email, string emailType)
    {
        TypeId = "EMAIL";
        Value = email;
        ValueType = emailType;
    }

    [JsonIgnore]
    public string Email => Value;
}
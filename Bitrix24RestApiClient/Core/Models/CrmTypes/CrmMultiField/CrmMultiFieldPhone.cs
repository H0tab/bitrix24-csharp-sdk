using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Core.Models.CrmTypes.CrmMultiField;

public class CrmMultiFieldPhone: CrmMultiField
{
    public CrmMultiFieldPhone() { }
    public CrmMultiFieldPhone(string phone, string phoneType)
    {
        TypeId = "PHONE";
        Value = phone;
        ValueType = phoneType;
    }

    [JsonIgnore]
    public string Phone => Value;
}
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Builders.Interfaces;
using Bitrix24RestApiClient.Core.Models.CrmTypes.CrmMultiField;

namespace Bitrix24RestApiClient.Core.Builders;

public class EmailListBuilder: IEmailListBuilder
{
    private readonly List<CrmMultiField> fields = new();

    public IEmailListBuilder SetField(string email, string type = EmailType.Рабочий)
    {
        fields.Add(new CrmMultiFieldEmail(email, type));
        return this;
    }

    public List<CrmMultiField> Build() => fields;
}
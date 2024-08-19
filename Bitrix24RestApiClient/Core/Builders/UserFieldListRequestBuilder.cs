namespace Bitrix24RestApiClient.Core.Builders;

public class UserFieldListRequestBuilder
{
    private readonly Dictionary<string, string> fieldsToAddOrUpdate = new();

    public UserFieldListRequestBuilder SetField(string fieldName, string value)
    {
        fieldsToAddOrUpdate[fieldName] = value;
        return this;
    }

    public Dictionary<string, string> Get() => fieldsToAddOrUpdate;
}
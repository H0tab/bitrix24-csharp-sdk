using System.Linq.Expressions;
using Bitrix24RestApiClient.Core.Builders.Interfaces;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Models.RequestArgs;
using Bitrix24RestApiClient.Core.Utilities;

namespace Bitrix24RestApiClient.Core.Builders;

public class AddRequestBuilder<TEntity>: IAddRequestBuilder<TEntity>
{
    private int? entityTypeId;
    private readonly Dictionary<string, object> fields = new();
    private readonly PhoneListBuilder phonesBuilder = new();
    private readonly EmailListBuilder emailsBuilder = new();

    public IAddRequestBuilder<TEntity> SetEntityTypeId(EntityTypeIdEnum entityTypeId)
    {
        this.entityTypeId = entityTypeId.EntityTypeId;
        return this;
    }

    public IAddRequestBuilder<TEntity> SetEntityTypeId(int? entityTypeId)
    {
        this.entityTypeId = entityTypeId;
        return this;
    }
    
    public IAddRequestBuilder<TEntity> SetField(Expression<Func<TEntity, object>> fieldNameExpr, object value)
    {
        fields[fieldNameExpr.JsonPropertyName()] = fieldNameExpr.MapValue(value);
        return this;
    }

    public IAddRequestBuilder<TEntity> AddPhones(Action<IPhoneListBuilder> builderFunc)
    {
        builderFunc(phonesBuilder);
        return this;
    }

    public IAddRequestBuilder<TEntity> AddEmails(Action<IEmailListBuilder> builderFunc)
    {
        builderFunc(emailsBuilder);
        return this;
    }

    public CrmEntityAddArgs BuildArgs()
    {
        var phones = phonesBuilder.Build();
        if (phones.Count > 0)
            fields["PHONE"] = phones;

        var emails = emailsBuilder.Build();
        if (emails.Count > 0)
            fields["EMAIL"] = emails;

        return new CrmEntityAddArgs
        {
            EntityTypeId = entityTypeId,
            Fields = fields
        };
    }
}
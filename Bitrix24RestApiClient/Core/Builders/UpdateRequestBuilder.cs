using System.Linq.Expressions;
using Bitrix24RestApiClient.Core.Utilities;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Builders.Interfaces;
using Bitrix24RestApiClient.Core.Models.RequestArgs.CrmEntityUpdateArgs;

namespace Bitrix24RestApiClient.Core.Builders;

public class UpdateRequestBuilder<TEntity, TArgs>: IUpdateRequestBuilder<TEntity, TArgs> where TArgs : IUpdateArgs, new()
{
    private int? entityTypeId;
    private int id;
    private readonly Dictionary<string, object> fields = new();
    private readonly PhoneListBuilder phonesBuilder = new();
    private readonly EmailListBuilder emailsBuilder = new();

    public IUpdateRequestBuilder<TEntity, TArgs> SetId(int id)
    {
        this.id = id;
        return this;
    }

    public IUpdateRequestBuilder<TEntity, TArgs> SetEntityTypeId(EntityTypeIdEnum entityTypeId)
    {
        this.entityTypeId = entityTypeId.EntityTypeId;
        return this;
    }

    public IUpdateRequestBuilder<TEntity, TArgs> SetEntityTypeId(int? entityTypeId)
    {
        this.entityTypeId = entityTypeId;
        return this;
    }

    public IUpdateRequestBuilder<TEntity, TArgs> SetField(Expression<Func<TEntity, object>> fieldNameExpr, object value)
    {
        fields[fieldNameExpr.JsonPropertyName()] = value;
        return this;
    }

    public IUpdateRequestBuilder<TEntity, TArgs> AddPhones(Action<IPhoneListBuilder> builderFunc)
    {
        builderFunc(phonesBuilder);
        return this;
    }

    public IUpdateRequestBuilder<TEntity, TArgs> AddEmails(Action<IEmailListBuilder> builderFunc)
    {
        builderFunc(emailsBuilder);
        return this;
    }

    public TArgs BuildArgs()
    {
        var phones = phonesBuilder.Build();
        if (phones.Count > 0)
            fields["PHONE"] = phones;

        var emails = emailsBuilder.Build();
        if (emails.Count > 0)
            fields["EMAIL"] = emails;

        return new TArgs
        {
            EntityTypeId = entityTypeId,
            Id = id,
            Fields = fields
        };
    }
}
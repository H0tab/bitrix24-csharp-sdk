﻿using System.Linq.Expressions;
using Bitrix24RestApiClient.Core.Utilities;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Builders.Interfaces;
using Bitrix24RestApiClient.Core.Models.RequestArgs.CrmEntityUpdateArgs;

namespace Bitrix24RestApiClient.Core.Builders;

public class UpdateRequestBuilder<TEntity>: IUpdateRequestBuilder<TEntity>
{
    private int? entityTypeId;
    private int id;
    private readonly Dictionary<string, object> fields = new();
    private readonly PhoneListBuilder phonesBuilder = new();
    private readonly EmailListBuilder emailsBuilder = new();

    public IUpdateRequestBuilder<TEntity> SetId(int id)
    {
        this.id = id;
        return this;
    }

    public IUpdateRequestBuilder<TEntity> SetEntityTypeId(EntityTypeIdEnum entityTypeId)
    {
        this.entityTypeId = entityTypeId.EntityTypeId;
        return this;
    }

    public IUpdateRequestBuilder<TEntity> SetEntityTypeId(int? entityTypeId)
    {
        this.entityTypeId = entityTypeId;
        return this;
    }

    public IUpdateRequestBuilder<TEntity> SetField(Expression<Func<TEntity, object>> fieldNameExpr, object value)
    {
        fields[fieldNameExpr.JsonPropertyName()] = value;
        return this;
    }

    public IUpdateRequestBuilder<TEntity> AddPhones(Action<IPhoneListBuilder> builderFunc)
    {
        builderFunc(phonesBuilder);
        return this;
    }

    public IUpdateRequestBuilder<TEntity> AddEmails(Action<IEmailListBuilder> builderFunc)
    {
        builderFunc(emailsBuilder);
        return this;
    }

    public object BuildArgs(EntryPointPrefix entityTypePrefix)
    {
        var phones = phonesBuilder.Build();
        if (phones.Count > 0)
            fields["PHONE"] = phones;

        var emails = emailsBuilder.Build();
        if (emails.Count > 0)
            fields["EMAIL"] = emails;

        // Так люто, потому что для разных сущностей Id нужно передавать в разном регистре
        if (entityTypePrefix.Value == EntryPointPrefix.Item.Value)
        {
            return new CrmEntityUpdateArgsForItem
            {
                EntityTypeId = entityTypeId,
                Id = id,
                Fields = fields
            };
        }

        return new CrmEntityUpdateArgs
        {
            EntityTypeId = entityTypeId,
            Id = id,
            Fields = fields
        };
    }
}
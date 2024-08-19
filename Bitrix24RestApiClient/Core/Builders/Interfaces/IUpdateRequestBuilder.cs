﻿using System.Linq.Expressions;

namespace Bitrix24RestApiClient.Core.Builders.Interfaces;

public interface IUpdateRequestBuilder<TEntity>
{
    IUpdateRequestBuilder<TEntity> SetId(int id);
    IUpdateRequestBuilder<TEntity> SetField(Expression<Func<TEntity, object>> fieldNameExpr, object value);
    IUpdateRequestBuilder<TEntity> AddPhones(Action<IPhoneListBuilder> builderFunc);
    IUpdateRequestBuilder<TEntity> AddEmails(Action<IEmailListBuilder> builderFunc);
}
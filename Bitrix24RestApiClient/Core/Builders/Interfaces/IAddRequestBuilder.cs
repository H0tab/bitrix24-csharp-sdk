﻿using System.Linq.Expressions;

namespace Bitrix24RestApiClient.Core.Builders.Interfaces;

public interface IAddRequestBuilder<TEntity>
{
    IAddRequestBuilder<TEntity> SetField(Expression<Func<TEntity, object>> fieldNameExpr, object value);
    IAddRequestBuilder<TEntity> AddPhones(Action<IPhoneListBuilder> builderFunc);
    IAddRequestBuilder<TEntity> AddEmails(Action<IEmailListBuilder> builderFunc);
}
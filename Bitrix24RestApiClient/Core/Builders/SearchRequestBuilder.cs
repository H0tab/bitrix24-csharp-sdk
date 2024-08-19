﻿using System.Linq.Expressions;
using Bitrix24RestApiClient.Core.Models;
using Bitrix24RestApiClient.Core.Utilities;
using Bitrix24RestApiClient.Core.Models.RequestArgs;
using Bitrix24RestApiClient.Core.Builders.Interfaces;

namespace Bitrix24RestApiClient.Core.Builders;

public class SearchRequestBuilder<TEntity>: ISearchRequestBuilder<TEntity>
{
    private readonly List<Filter> filter = new();

    public ISearchRequestBuilder<TEntity> AddFilter(Expression<Func<TEntity, object>> nameExpr, object value, FilterOperator op = FilterOperator.Default)
    {
        filter.Add(new Filter
        {
            Name = nameExpr.JsonPropertyName(),
            Value = value?.ToString(),
            Operator = op
        });
        return this;
    }

    public CrmSearchRequestArgs BuildArgs() => new(filter);
}
using System.Linq.Expressions;
using Bitrix24RestApiClient.Core.Models;

namespace Bitrix24RestApiClient.Core.Builders.Interfaces;

public interface ISearchRequestBuilder<TEntity>
{
    ISearchRequestBuilder<TEntity> AddFilter(Expression<Func<TEntity, object>> nameExpr, object value, FilterOperator op = FilterOperator.Default);
}
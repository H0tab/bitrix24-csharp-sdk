using System.Linq.Expressions;
using Bitrix24RestApiClient.Core.Models;

namespace Bitrix24RestApiClient.Core.Builders.Interfaces;

public interface IStageHistoriesListRequestBuilder<TEntity>
{
    IListRequestBuilder<TEntity> SetStart(int start);
    IListRequestBuilder<TEntity> AddOrderBy(Expression<Func<TEntity, object>> fieldExpr);
    IListRequestBuilder<TEntity> AddOrderByDesc(Expression<Func<TEntity, object>> fieldExpr);
    IListRequestBuilder<TEntity> AddFilter(Expression<Func<TEntity, object>> nameExpr, object value, FilterOperator op = FilterOperator.Default);
}
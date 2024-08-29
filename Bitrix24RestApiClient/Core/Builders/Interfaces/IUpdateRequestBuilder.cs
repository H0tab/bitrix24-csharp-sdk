using System.Linq.Expressions;
using Bitrix24RestApiClient.Core.Models.RequestArgs.CrmEntityUpdateArgs;

namespace Bitrix24RestApiClient.Core.Builders.Interfaces;

public interface IUpdateRequestBuilder<TEntity, out TArgs> where TArgs : IUpdateArgs
{
    IUpdateRequestBuilder<TEntity, TArgs> SetId(int id);
    IUpdateRequestBuilder<TEntity, TArgs> SetField(Expression<Func<TEntity, object>> fieldNameExpr, object value);
    IUpdateRequestBuilder<TEntity, TArgs> AddPhones(Action<IPhoneListBuilder> builderFunc);
    IUpdateRequestBuilder<TEntity, TArgs> AddEmails(Action<IEmailListBuilder> builderFunc);
    TArgs BuildArgs();
}
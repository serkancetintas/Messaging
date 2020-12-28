using Armut.Messaging.Infrastructure.Types;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Armut.Messaging.Infrastructure.Mongo
{
    public interface IMongoRepository<TEntity, in TIdentifiable> where TEntity : IIdentifiable<TIdentifiable>
	{
		IMongoCollection<TEntity> Collection { get; }
		Task<TEntity> GetAsync(TIdentifiable id);
		Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
		Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
		Task<IReadOnlyList<TEntity>> FindAndSortByAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> sort);
		Task<IReadOnlyList<TEntity>> FindAndSortByDescAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> sort);
		Task AddAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
		Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate);
		Task DeleteAsync(TIdentifiable id);
		Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
		Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
	}
}

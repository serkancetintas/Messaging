using Armut.Messaging.Infrastructure.Types;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Armut.Messaging.Infrastructure.Mongo.Repositories
{
    public class MongoRepository<TEntity, TIdentifiable> : IMongoRepository<TEntity, TIdentifiable>
          where TEntity : IIdentifiable<TIdentifiable>
    {
        public IMongoCollection<TEntity> Collection { get; }

        public MongoRepository(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            Collection = database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
        }
        private string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

		public Task<TEntity> GetAsync(TIdentifiable id)
			=> GetAsync(e => e.Id.Equals(id));

		public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
			=> Collection.Find(predicate).SingleOrDefaultAsync();

		public async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
			=> await Collection.Find(predicate).ToListAsync();

		public async Task<IReadOnlyList<TEntity>> FindAndSortByAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> sort)
			=> await Collection.Find(predicate).SortBy(sort).ToListAsync();

		public async Task<IReadOnlyList<TEntity>> FindAndSortByDescAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> sort)
		=> await Collection.Find(predicate).SortByDescending(sort).ToListAsync();

		public Task AddAsync(TEntity entity)
			=> Collection.InsertOneAsync(entity);

		public Task UpdateAsync(TEntity entity)
			=> UpdateAsync(entity, e => e.Id.Equals(entity.Id));

		public Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
			=> Collection.ReplaceOneAsync(predicate, entity);

		public Task DeleteAsync(TIdentifiable id)
			=> DeleteAsync(e => e.Id.Equals(id));

		public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
			=> Collection.DeleteOneAsync(predicate);

		public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
			=> Collection.Find(predicate).AnyAsync();
	}
}

using MongoDB.Bson;
using MongoDB.Driver;
using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.Exceptions;
using System.Linq.Expressions;

namespace RestFit.DataAccess
{
    public abstract class BaseAccess<TDocument> : IDocumentAccess<TDocument> where TDocument : class
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        protected IMongoCollection<TDocument> Collection { get; }

        public BaseAccess(string name)
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _database = _client.GetDatabase("RestFit");
            Collection = _database.GetCollection<TDocument>(name);
            EnsureIndices();
            EnsureViews();
        }

        protected void CreateIndex(bool unique, bool sparse, Expression < Func<TDocument, object>> action)
        {
            var indexOptions = new CreateIndexOptions
            {
                Sparse = sparse,
                Unique = unique
            };

            var combined = new List<IndexKeysDefinition<TDocument>>
            {
                Builders<TDocument>.IndexKeys.Ascending(action)
            };

            var indexDefinition = Builders<TDocument>.IndexKeys.Combine(combined);
            var createIndexModel = new CreateIndexModel<TDocument>(indexDefinition, indexOptions);
            Collection.Indexes.CreateOneAsync(createIndexModel);
        }

        protected void CreateIndex(bool unique, bool sparse, params Expression<Func<TDocument, object>>[] actions)
        {
            var indexOptions = new CreateIndexOptions
            {
                Sparse = sparse,
                Unique = unique
            };

            var combined = actions.Select(x => Builders<TDocument>.IndexKeys.Ascending(x)).ToList();

            var indexDefinition = Builders<TDocument>.IndexKeys.Combine(combined);
            var createIndexModel = new CreateIndexModel<TDocument>(indexDefinition, indexOptions);
            Collection.Indexes.CreateOneAsync(createIndexModel);
        }

        protected abstract void EnsureIndices();
        protected abstract void EnsureViews();

        public async Task InsertDocumentAsync(TDocument document)
        {
            try
            {
                await Collection.InsertOneAsync(document).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new DataAccessMongoDbException("Insert failed. See inner exception for more details", ex);
            }
        }

        public async Task<ICollection<TDocument>> RetrieveDocumentsAsync(FilterDefinition<TDocument>? filterDefinition = null)
        {
            try
            {
                var documents = await Collection.FindAsync(filterDefinition ?? new BsonDocument()).ConfigureAwait(false);
                return documents.ToList();
            }
            catch (Exception ex)
            {
                throw new DataAccessMongoDbException("Insert failed. See inner exception for more details", ex);
            }
        }

        public async Task<long> CountDocumentsAsync(FilterDefinition<TDocument>? filterDefinition = null)
        {
            try
            {
                return await Collection.CountDocumentsAsync(filterDefinition).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new DataAccessMongoDbException("Count failed. See inner exception for more details", ex);
            }
        }

        public async Task UpdateAsync(FilterDefinition<TDocument>? filterDefinition = null, UpdateDefinition<TDocument>? updateDefinition = null)
        {
            try
            {
                await Collection.UpdateManyAsync(filterDefinition, updateDefinition).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new DataAccessMongoDbException("Update failed. See inner exception for more details", ex);
            }
        }

        public async Task<bool> ExistsAsync(FilterDefinition<TDocument>? filterDefinition = null)
        {
            try
            {
                return await Collection.CountDocumentsAsync(filterDefinition, new CountOptions { Limit = 1 }).ConfigureAwait(false) == 1;
            }
            catch (Exception ex)
            {
                throw new DataAccessMongoDbException("Exists failed. See inner exception for more details", ex);
            }
        }
    }
}

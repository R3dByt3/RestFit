using MongoDB.Bson;
using MongoDB.Driver;
using RestFit.Repository.Abstract;
using System.Linq.Expressions;

namespace RestFit.Repository
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

        public void InsertDocument(TDocument document)
        {
            Collection.InsertOne(document);
        }

        public ICollection<TDocument> RetrieveDocuments(FilterDefinition<TDocument>? filterDefinition = null)
        {
            return Collection.Find(filterDefinition ?? new BsonDocument()).ToList();
        }

        public long CountDocuments(FilterDefinition<TDocument>? filterDefinition = null)
        {
            return Collection.CountDocuments(filterDefinition);
        }
    }
}

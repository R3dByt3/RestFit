using MongoDB.Driver;

namespace RestFit.DataAccess.Abstract
{
    public interface IDocumentAccess<TDocument>
    {
        public Task<ICollection<TDocument>> RetrieveDocumentsAsync(FilterDefinition<TDocument>? filterDefinition = null);
        public Task InsertDocumentAsync(TDocument document);
        public Task<long> CountDocumentsAsync(FilterDefinition<TDocument>? filterDefinition = null);
        public Task UpdateAsync(FilterDefinition<TDocument>? filterDefinition = null, UpdateDefinition<TDocument>? updateDefinition = null);
        public Task<bool> ExistsAsync(FilterDefinition<TDocument>? filterDefinition = null);
    }
}

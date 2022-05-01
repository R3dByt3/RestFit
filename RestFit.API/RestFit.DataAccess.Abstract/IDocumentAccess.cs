using MongoDB.Driver;

namespace RestFit.DataAccess.Abstract
{
    public interface IDocumentAccess<TDocument>
    {
        public ICollection<TDocument> RetrieveDocuments(FilterDefinition<TDocument>? filterDefinition = null);
        public void InsertDocument(TDocument document);
        public long CountDocuments(FilterDefinition<TDocument>? filterDefinition = null);
        public void Update(FilterDefinition<TDocument>? filterDefinition = null, UpdateDefinition<TDocument>? updateDefinition = null);
        public bool Exists(FilterDefinition<TDocument>? filterDefinition = null);
    }
}

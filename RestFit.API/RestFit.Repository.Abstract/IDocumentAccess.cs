using MongoDB.Driver;

namespace RestFit.Repository.Abstract
{
    public interface IDocumentAccess<TDocument>
    {
        public ICollection<TDocument> RetrieveDocuments(FilterDefinition<TDocument>? filterDefinition = null);
        public void InsertDocument(TDocument document);
        public long CountDocuments(FilterDefinition<TDocument>? filterDefinition = null);
    }
}

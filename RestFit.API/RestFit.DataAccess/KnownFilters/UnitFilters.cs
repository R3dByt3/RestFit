using MongoDB.Driver;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess.KnownFilters
{
    public static class UnitFilters
    {
        public static readonly FilterDefinitionBuilder<Unit> Filter = Builders<Unit>.Filter;

        public static readonly FilterDefinition<Unit> Empty = Filter.Empty;

        public static FilterDefinition<Unit> GetById(string? id) => Filter.Eq(x => x.Id, id);

        public static FilterDefinition<Unit> GetByUserId(string? userId) => Filter.Eq(x => x.UserId, userId);

        public static FilterDefinition<Unit> GetByType(string? type) => Filter.Eq(x => x.Type, type);
    }
}

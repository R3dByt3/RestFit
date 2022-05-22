using MongoDB.Driver;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess.KnownFilters
{
    public static class HealthUnitFilters
    {
        public static readonly FilterDefinitionBuilder<HealthUnit> Filter = Builders<HealthUnit>.Filter;

        public static readonly FilterDefinition<HealthUnit> Empty = Filter.Empty;

        public static FilterDefinition<HealthUnit> GetById(string? id) => Filter.Eq(x => x.Id, id);
        public static FilterDefinition<HealthUnit> GetByUserId(string? userId) => Filter.Eq(x => x.UserId, userId);
    }
}

using MongoDB.Driver;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess.KnownFilters
{
    public static class UserGroupedUnitFilters
    {
        public static readonly FilterDefinitionBuilder<UserGroupedUnit> Filter = Builders<UserGroupedUnit>.Filter;

        public static readonly FilterDefinition<UserGroupedUnit> Empty = Filter.Empty;

        public static FilterDefinition<UserGroupedUnit> GetByUserId(string? userId) => Filter.Eq(x => x.UserId, userId);
    }
}

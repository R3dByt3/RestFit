using MongoDB.Driver;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess.KnownFilters
{
    public static class UserFilters
    {
        public static FilterDefinitionBuilder<User> Filter = Builders<User>.Filter;

        public static FilterDefinition<User> Empty = Filter.Empty;

        public static FilterDefinition<User> GetById(string id) => Filter.Eq(x => x.Id, id);

        public static FilterDefinition<User> GetByUsername(string username) => Filter.Eq(x => x.Username, username);

        public static FilterDefinition<User> GetByPassword(string password) => Filter.Eq(x => x.Password, password);
    }
}

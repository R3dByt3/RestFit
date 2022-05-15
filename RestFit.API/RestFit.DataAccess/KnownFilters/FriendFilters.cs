using MongoDB.Driver;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess.KnownFilters
{
    public static class FriendFilters
    {
        public static readonly FilterDefinitionBuilder<Friend> Filter = Builders<Friend>.Filter;

        public static readonly FilterDefinition<Friend> Empty = Filter.Empty;

        public static FilterDefinition<Friend> GetById(string? id) => Filter.Eq(x => x.Id, id);

        public static FilterDefinition<Friend> GetByUserId(string? userId) => Filter.Eq(x => x.UserId, userId);

        public static FilterDefinition<Friend> GetByFriendId(string? friendId) => Filter.Eq(x => x.FriendId, friendId);
    }

    //ToDo: DELETE
}

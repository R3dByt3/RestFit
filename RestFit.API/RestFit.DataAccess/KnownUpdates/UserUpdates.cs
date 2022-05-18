using MongoDB.Driver;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess.KnownUpdates
{
    public static class UserUpdates
    {
        public static readonly UpdateDefinitionBuilder<User> Update = Builders<User>.Update;

        public static UpdateDefinition<User> AddPendingInFriendRequestUserIds(string userId) => Update
            .AddToSet(x => x.PendingInFriendRequestUserIds, userId);
        public static UpdateDefinition<User> AddPendingOutFriendRequestUserIds(string userId) => Update
            .AddToSet(x => x.PendingOutFriendRequestUserIds, userId);
    }
}

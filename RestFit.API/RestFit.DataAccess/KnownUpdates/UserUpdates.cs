using MongoDB.Driver;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess.KnownUpdates
{
    public static class UserUpdates
    {
        public static readonly UpdateDefinitionBuilder<User> Update = Builders<User>.Update;

        public static UpdateDefinition<User> AddPendingInFriendRequestUserId(string userId) => Update
            .AddToSet(x => x.PendingInFriendRequestUserIds, userId);
        public static UpdateDefinition<User> AddPendingOutFriendRequestUserId(string userId) => Update
            .AddToSet(x => x.PendingOutFriendRequestUserIds, userId);
        public static UpdateDefinition<User> AddFriendUserId(string userId) => Update
            .AddToSet(x => x.FriendUserIds, userId);
        public static UpdateDefinition<User> RemovePendingInFriendRequestUserId(string userId) => Update
            .PullFilter(x => x.PendingInFriendRequestUserIds, userId);
        public static UpdateDefinition<User> RemovePendingOutFriendRequestUserId(string userId) => Update
            .PullFilter(x => x.PendingOutFriendRequestUserIds, userId);
    }
}

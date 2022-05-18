using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess.ClassMaps
{
    public static class UserClassMap
    {
        public static void Init() => BsonClassMap.RegisterClassMap<User>(cm =>
        {
            cm.MapIdField(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetElementName("id");

            cm.MapField(c => c.Username)
                .SetElementName("username");

            cm.MapField(c => c.Password)
                .SetElementName("password");

            cm.MapField(c => c.PendingInFriendRequestUserIds)
                .SetElementName("pending_in_friend_request_user_ids");

            cm.MapField(c => c.PendingOutFriendRequestUserIds)
                .SetElementName("pending_out_friend_request_user_ids");

            cm.MapField(c => c.FriendUserIds)
                .SetElementName("friend_user_ids");
        });
    }
}

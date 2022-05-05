﻿using MongoDB.Bson.Serialization;
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
        });
    }
}

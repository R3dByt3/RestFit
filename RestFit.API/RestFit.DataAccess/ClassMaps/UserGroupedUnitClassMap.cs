using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess.ClassMaps
{
    public static class UserGroupedUnitClassMap
    {
        public static void Init() => BsonClassMap.RegisterClassMap<UserGroupedUnit>(cm =>
        {
            cm.MapIdField(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetElementName("id");

            cm.MapField(c => c.UserId)
                .SetElementName("user_id");

            cm.MapField(c => c.SetsSum)
                .SetElementName("sets_sum");

            cm.MapField(c => c.RepetitionsSum)
                .SetElementName("repitions_sum");

            cm.MapField(c => c.WeightsSum)
                .SetElementName("weights_sum");

            cm.MapField(c => c.DocumentCount)
                .SetElementName("document_count");
        });
    }
}

using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess.ClassMaps
{
    public static class UnitClassMap
    {
        public static void Init() => BsonClassMap.RegisterClassMap<Unit>(cm =>
        {
            cm.MapIdField(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetElementName("id");

            cm.MapField(c => c.UserId)
                .SetElementName("user_id");

            cm.MapField(c => c.Type)
                .SetElementName("type");

            cm.MapField(c => c.Sets)
                .SetElementName("sets");

            cm.MapField(c => c.Repetitions)
                .SetElementName("repetitions");

            cm.MapField(c => c.Weight)
                .SetElementName("weight");

            cm.MapField(c => c.Comment)
                .SetElementName("comment");

            cm.MapField(c => c.ProcessedFor)
                .SetElementName("processed_for");
        });
    }
}

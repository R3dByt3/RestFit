using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace RestFit.Repository.Abstract.ClassMaps
{
    public static class UnitClassMap
    {
        public static void Init() => BsonClassMap.RegisterClassMap<Unit>(cm =>
        {
            cm.MapIdField(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetElementName("id");

            cm.MapField(c => c.Type)
                .SetElementName("type");

            cm.MapField(c => c.Repitions)
                .SetElementName("repitions");

            cm.MapField(c => c.Duration)
                .SetElementName("duration")
                .SetSerializer(new TimeSpanSerializer());
        });
    }
}

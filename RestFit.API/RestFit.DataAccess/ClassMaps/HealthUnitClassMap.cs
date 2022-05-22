using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess.ClassMaps
{
    public static class HealthUnitClassMap
    {
        public static void Init() => BsonClassMap.RegisterClassMap<HealthUnit>(cm =>
        {
            cm.MapIdField(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetElementName("id");

            cm.MapField(c => c.UserId)
                .SetElementName("user_id");

            cm.MapField(c => c.WaistSize)
                .SetElementName("waist_size");

            cm.MapField(c => c.Weight)
                .SetElementName("weight");

            cm.MapField(c => c.ArmSize)
                .SetElementName("arm_size");

            cm.MapField(c => c.HipSize)
                .SetElementName("hip_size");

            cm.MapField(c => c.ThightSize)
                .SetElementName("thight_size");

            cm.MapField(c => c.DateUtc)
                .SetElementName("date_utc");
        });
    }
}

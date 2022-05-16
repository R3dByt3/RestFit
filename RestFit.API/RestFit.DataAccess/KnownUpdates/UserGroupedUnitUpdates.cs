using MongoDB.Driver;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess.KnownUpdates
{
    public static class UserGroupedUnitUpdates
    {
        public static readonly UpdateDefinitionBuilder<UserGroupedUnit> Update = Builders<UserGroupedUnit>.Update;

        public static UpdateDefinition<UserGroupedUnit> UpdateAggregations(long repetitionsSum, long documentCount, long setsSum, double weightsSum) => Update
            .Inc(x => x.RepetitionsSum, repetitionsSum)
            .Inc(x => x.DocumentCount, documentCount)
            .Inc(x => x.SetsSum, setsSum)
            .Inc(x => x.WeightsSum, weightsSum);
    }
}

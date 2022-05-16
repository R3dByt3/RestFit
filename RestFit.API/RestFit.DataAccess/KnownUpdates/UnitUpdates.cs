using MongoDB.Driver;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess.KnownUpdates
{
    public static class UnitUpdates
    {
        public static readonly UpdateDefinitionBuilder<Unit> Update = Builders<Unit>.Update;

        public static UpdateDefinition<Unit> SetProcessedByForUnits(string processorName) => Update
            .AddToSet(x => x.ProcessedFor, processorName);
    }
}

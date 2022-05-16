using MongoDB.Driver;
using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess.KnownFilters
{
    public static class UnitFilters
    {
        public static readonly FilterDefinitionBuilder<Unit> Filter = Builders<Unit>.Filter;
        public static readonly ProjectionDefinitionBuilder<Unit> Projection = Builders<Unit>.Projection;

        public static readonly FilterDefinition<Unit> Empty = Filter.Empty;

        public static FilterDefinition<Unit> GetById(string? id) => Filter.Eq(x => x.Id, id);
        public static FilterDefinition<Unit> GetByUserId(string? userId) => Filter.Eq(x => x.UserId, userId);
        public static FilterDefinition<Unit> GetByType(string? type) => Filter.Eq(x => x.Type, type);
        public static FilterDefinition<Unit> GetIfNotProcessedBy(string? type) => Filter.Not(Filter.AnyEq(x => x.ProcessedFor, type));

        public static PipelineDefinition<Unit, UserGroupedUnit> GetUnitGroups(string processorName) => new IPipelineStageDefinition[]
        {
            PipelineStageDefinitionBuilder.Match(GetIfNotProcessedBy(processorName)),
            PipelineStageDefinitionBuilder.Group<Unit, string, UserGroupedUnit>(x => x.UserId, x => 
            new UserGroupedUnit
            {
                UserId = x.First().UserId,
                RepetitionsSum = x.Sum(v => v.Repetitions),
                SetsSum = x.Sum(v => v.Sets),
                WeightsSum = x.Sum(v => v.Weight),
                DocumentCount = x.Count(),
                DocumentIds = x.Select(v => v.Id),
            })
        };
    }
}

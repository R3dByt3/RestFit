using RestFit.API.Controllers.v1.Mappers;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract.KnownSearches;
using RestFit.Logic.Abstract;

namespace RestFit.API.Controllers.v1
{
    public class UnitControllerV1Processor : IUnitControllerV1Processor
    {
        private readonly IProcessorHub _processorHub;

        public UnitControllerV1Processor(IProcessorHub processorHub)
        {
            _processorHub = processorHub;
        }

        public async Task CreateUnitAsync(UnitDto unit)
        {
            await _processorHub.InsertProcessor.CreateUnitAsync(UnitDtoMapper.Instance.Convert(unit));
        }

        public async Task<IEnumerable<UnitDto>> GetUnitsAsync(UnitSearchDto? searchDto = null)
        {
            var search = UnitSearchDtoMapper.Instance.Convert(searchDto);
            var units = await _processorHub.SearchProcessor.GetUnits().ConfigureAwait(false);
            return units.Select(x => UnitDtoMapper.Instance.Convert(x));
        }
    }
}

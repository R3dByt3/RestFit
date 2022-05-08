using RestFit.API.Controllers.v1.Mappers;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract.KnownSearches;
using RestFit.Logic.Abstract;
using System.Security.Claims;

namespace RestFit.API.Controllers.v1
{
    public class UnitControllerV1Processor : IUnitControllerV1Processor
    {
        private readonly IProcessorHub _processorHub;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitControllerV1Processor(IProcessorHub processorHub, IHttpContextAccessor httpContextAccessor)
        {
            _processorHub = processorHub;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetCurrentUserId() => _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        public async Task CreateUnitAsync(UnitDto unitDto)
        {
            unitDto = unitDto with { UserId = GetCurrentUserId() };
            await _processorHub.InsertProcessor.CreateUnitAsync(UnitDtoMapper.Instance.Convert(unitDto));
        }

        public async Task<IEnumerable<UnitDto>> GetUnitsAsync(UnitSearchDto? searchDto = null)
        {
            if (searchDto == null)
                searchDto = new UnitSearchDto();
            searchDto.UserId = GetCurrentUserId();

            var search = UnitSearchDtoMapper.Instance.Convert(searchDto);
            var units = await _processorHub.SearchProcessor.GetUnits(search).ConfigureAwait(false);
            return units.Select(x => UnitDtoMapper.Instance.Convert(x));
        }
    }
}

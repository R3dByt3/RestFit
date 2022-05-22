using RestFit.API.Controllers.v1.Mappers;
using RestFit.Client.Abstract.KnownSearches;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract;
using RestFit.Logic.Abstract;
using System.Security.Claims;

namespace RestFit.API.Controllers.v1
{
    public class HealthUnitControllerV1Processor : IHealthUnitControllerV1Processor
    {
        private readonly IProcessorHub _processorHub;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HealthUnitControllerV1Processor(IProcessorHub processorHub, IHttpContextAccessor httpContextAccessor)
        {
            _processorHub = processorHub;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetCurrentUserId() => _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        public async Task<HealthUnitDto> CreateHealthUnitAsync(HealthUnitDto HealthUnitDto)
        {
            HealthUnitDto = HealthUnitDto with { UserId = GetCurrentUserId() };
            var HealthUnit = HealthUnitDtoMapper.Instance.Convert(HealthUnitDto);
            await _processorHub.InsertProcessor.CreateHealthUnitAsync(HealthUnit).ConfigureAwait(false);
            return HealthUnitDtoMapper.Instance.Convert(HealthUnit);
        }

        public async Task<IEnumerable<HealthUnitDto>> GetHealthUnitsAsync(HealthUnitSearchDto? searchDto = null)
        {
            if (searchDto == null)
                searchDto = new HealthUnitSearchDto();
            searchDto.UserId = GetCurrentUserId();

            var search = HealthUnitSearchDtoMapper.Instance.Convert(searchDto);
            var HealthUnits = await _processorHub.SearchProcessor.GetHealthUnitsAsync(search).ConfigureAwait(false);
            return HealthUnits.Select(x => HealthUnitDtoMapper.Instance.Convert(x));
        }
    }
}

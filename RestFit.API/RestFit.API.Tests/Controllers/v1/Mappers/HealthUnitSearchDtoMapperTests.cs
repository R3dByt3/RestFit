using FluentAssertions;
using RestFit.API.Controllers.v1.Mappers;
using RestFit.Client.Abstract.KnownSearches;
using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.API.Tests.Controllers.v1.Mappers
{
    [TestFixture]
    public class HealthUnitSearchDtoMapperTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;

        private HealthUnitSearchDtoMapper _mapper = null!;
        private HealthUnitSearch _healthUnitSearch = null!;
        private HealthUnitSearchDto _healthUnitSearchDto = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _mapper = new();
            _healthUnitSearch = new HealthUnitSearch
            {
                DateUtc = Now,
                UserId = "hijklmno",
            };
            _healthUnitSearchDto = new HealthUnitSearchDto
            {
                DateUtc = Now,
                UserId = "hijklmno",
            };
        }

        [Test]
        public void ConvertDto_ShouldReturnExpected()
        {
            _mapper.Convert(_healthUnitSearchDto).Should().BeEquivalentTo(_healthUnitSearch, opt => opt
            .ExcludingMissingMembers()
            .Using<Dictionary<HealthUnitFields, string[]>>(_ => { })
                .WhenTypeIs<Dictionary<HealthUnitFields, string[]>>());
        }

        [Test]
        public void Convert_ShouldReturnExpectedDto()
        {
            _mapper.Convert(_healthUnitSearch).Should().BeEquivalentTo(_healthUnitSearchDto, opt => opt
            .ExcludingMissingMembers()
            .Using<Dictionary<HealthUnitFields, string[]>>(_ => { })
                .WhenTypeIs<Dictionary<HealthUnitFields, string[]>>());
        }
    }
}

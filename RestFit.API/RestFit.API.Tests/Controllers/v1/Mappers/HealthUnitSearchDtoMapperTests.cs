using FluentAssertions;
using RestFit.API.Controllers.v1.Mappers;
using RestFit.API.Extensions;
using RestFit.Client.Abstract.KnownSearches;
using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.API.Tests.Controllers.v1.Mappers
{
    [TestFixture]
    public class HealthUnitSearchDtoMapperTests
    {
        private static readonly DateTime Now = DateTime.UtcNow.TruncateDateTimeUtc();

        private HealthUnitSearchDtoMapper _mapper = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _mapper = new();
        }

        private static IEnumerable<TestCaseData> GetExamples()
        {
            yield return new TestCaseData(
                new HealthUnitSearch
                {
                    DateUtc = Now,
                    UserId = "hijklmno",
                }, 
                new HealthUnitSearchDto
                {
                    DateUtc = Now,
                    UserId = "hijklmno",
                }
                ).SetArgDisplayNames("Filled objects");
            yield return new TestCaseData(
                new HealthUnitSearch(),
                new HealthUnitSearchDto()
                ).SetArgDisplayNames("Empty objects");
        }

        [Test]
        [TestCaseSource(nameof(GetExamples))]
        public void ConvertDto_ShouldReturnExpected(HealthUnitSearch healthUnitSearch, HealthUnitSearchDto healthUnitSearchDto)
        {
            _mapper.Convert(healthUnitSearchDto).Should().BeEquivalentTo(healthUnitSearch, opt => opt
            .ExcludingMissingMembers()
            .Using<Dictionary<HealthUnitFields, string[]>>(_ => { })
                .WhenTypeIs<Dictionary<HealthUnitFields, string[]>>());
        }

        [Test]
        [TestCaseSource(nameof(GetExamples))]
        public void Convert_ShouldReturnExpectedDto(HealthUnitSearch healthUnitSearch, HealthUnitSearchDto healthUnitSearchDto)
        {
            _mapper.Convert(healthUnitSearch).Should().BeEquivalentTo(healthUnitSearchDto, opt => opt
            .ExcludingMissingMembers()
            .Using<Dictionary<HealthUnitFields, string[]>>(_ => { })
                .WhenTypeIs<Dictionary<HealthUnitFields, string[]>>());
        }
    }
}

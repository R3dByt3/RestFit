using FluentAssertions;
using RestFit.API.Controllers.v1.Mappers;
using RestFit.API.Extensions;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract;

namespace RestFit.API.Tests.Controllers.v1.Mappers
{
    [TestFixture]
    public class HealthUnitDtoMapperTests
    {
        private static readonly DateTime Now = DateTime.UtcNow.TruncateDateTimeUtc();

        private HealthUnitDtoMapper _mapper = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _mapper = new();
        }

        private static IEnumerable<TestCaseData> GetExamples()
        {
            yield return new TestCaseData(
                new HealthUnit
                {
                    ArmSize = 1.2,
                    DateUtc = Now,
                    HipSize = 2.3,
                    Id = "abcdfeg",
                    ThightSize = 3.4,
                    UserId = "hijklmno",
                    WaistSize = 4.5,
                    Weight = 5.6
                }, new HealthUnitDto
                {
                    ArmSize = 1.2,
                    DateUtc = Now,
                    HipSize = 2.3,
                    Id = "abcdfeg",
                    ThightSize = 3.4,
                    UserId = "hijklmno",
                    WaistSize = 4.5,
                    Weight = 5.6
                }
                ).SetArgDisplayNames("Filled objects");
        }

        [Test]
        [TestCaseSource(nameof(GetExamples))]
        public void ConvertDto_ShouldReturnExpected(HealthUnit healthUnit, HealthUnitDto healthUnitDto)
        {
            _mapper.Convert(healthUnitDto).Should().BeEquivalentTo(healthUnit);
        }

        [Test]
        [TestCaseSource(nameof(GetExamples))]
        public void Convert_ShouldReturnExpectedDto(HealthUnit healthUnit, HealthUnitDto healthUnitDto)
        {
            _mapper.Convert(healthUnit).Should().BeEquivalentTo(healthUnitDto);
        }
    }
}

using FluentAssertions;
using RestFit.API.Controllers.v1.Mappers;
using RestFit.API.Extensions;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract;

namespace RestFit.API.Tests.Controllers.v1.Mappers
{
    [TestFixture]
    public class UnitDtoMapperTests
    {
        private static readonly DateTime Now = DateTime.UtcNow.TruncateDateTimeUtc();

        private UnitDtoMapper _mapper = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _mapper = new();
        }

        private static IEnumerable<TestCaseData> GetExamples()
        {
            yield return new TestCaseData(
                new Unit
                {
                    DateUtc = Now,
                    Id = "abcdfeg",
                    UserId = "hijklmno",
                    Weight = 5.6,
                    Comment = "pqrstuvw",
                    ProcessedFor = new[]
                    {
                        "x",
                        "y",
                        "z"
                    },
                    Repetitions = 12,
                    Sets = 24,
                    Type = "12345"
                }, new UnitDto
                {
                    DateUtc = Now,
                    Id = "abcdfeg",
                    UserId = "hijklmno",
                    Weight = 5.6,
                    Comment = "pqrstuvw",
                    Repetitions = 12,
                    Sets = 24,
                    Type = "12345"
                }).SetArgDisplayNames("Filled objects");
        }

        [Test]
        [TestCaseSource(nameof(GetExamples))]
        public void ConvertDto_ShouldReturnExpected(Unit unit, UnitDto unitDto)
        {
            _mapper.Convert(unitDto).Should().BeEquivalentTo(unit, opt => opt
                .ExcludingMissingMembers()
                .Excluding(ctx => ctx.ProcessedFor));
        }

        [Test]
        [TestCaseSource(nameof(GetExamples))]
        public void Convert_ShouldReturnExpectedDto(Unit unit, UnitDto unitDto)
        {
            _mapper.Convert(unit).Should().BeEquivalentTo(unitDto, opt => opt
                .ExcludingMissingMembers());
        }
    }
}

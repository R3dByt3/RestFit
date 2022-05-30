using FluentAssertions;
using RestFit.API.Controllers.v1.Mappers;
using RestFit.Client.Abstract.KnownSearches;
using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.API.Tests.Controllers.v1.Mappers
{
    [TestFixture]
    public class UnitSearchDtoMapperTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;

        private UnitSearchDtoMapper _mapper = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _mapper = new();
        }

        private static IEnumerable<TestCaseData> GetExamples()
        {
            yield return new TestCaseData(
                new UnitSearch
                {
                    Id = "abcdfeg",
                    UserId = "hijklmno",
                    Type = "12345",
                    NotProcessedBy = "x",
                    Ids = new[]
                    {
                        "y",
                        "z"
                    }
                },
                new UnitSearchDto
                {
                    Id = "abcdfeg",
                    UserId = "hijklmno",
                    Type = "12345"
                }
                ).SetArgDisplayNames("Filled objects");
            yield return new TestCaseData(
                new UnitSearch(),
                new UnitSearchDto()
                ).SetArgDisplayNames("Empty objects");
        }

        [Test]
        [TestCaseSource(nameof(GetExamples))]
        public void ConvertDto_ShouldReturnExpected(UnitSearch unitSearch, UnitSearchDto unitSearchDto)
        {
            _mapper.Convert(unitSearchDto).Should().BeEquivalentTo(unitSearch, opt => opt
            .ExcludingMissingMembers()
            .Using<Dictionary<UnitFields, string[]>>(_ => { })
                .WhenTypeIs<Dictionary<UnitFields, string[]>>()
            .Using<string[]>(ctx => ctx.Subject.Should().NotBeSameAs(ctx.Expectation).And.BeEquivalentTo(ctx.Expectation))
                .WhenTypeIs<string[]>());
        }

        [Test]
        [TestCaseSource(nameof(GetExamples))]
        public void Convert_ShouldReturnExpectedDto(UnitSearch unitSearch, UnitSearchDto unitSearchDto)
        {
            _mapper.Convert(unitSearch).Should().BeEquivalentTo(unitSearchDto, opt => opt
            .ExcludingMissingMembers()
            .Using<Dictionary<UnitFields, string[]>>(_ => { })
                .WhenTypeIs<Dictionary<UnitFields, string[]>>()
            .Using<string[]>(ctx => ctx.Subject.Should().NotBeSameAs(ctx.Expectation).And.BeEquivalentTo(ctx.Expectation))
                .WhenTypeIs<string[]>());
        }
    }
}

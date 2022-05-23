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
        private UnitSearch _unitSearch = null!;
        private UnitSearchDto _unitSearchDto = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _mapper = new();
            _unitSearch = new UnitSearch
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
            };
            _unitSearchDto = new UnitSearchDto
            {
                Id = "abcdfeg",
                UserId = "hijklmno",
                Type = "12345"
            };
        }

        [Test]
        public void ConvertDto_ShouldReturnExpected()
        {
            _mapper.Convert(_unitSearchDto).Should().BeEquivalentTo(_unitSearch, opt => opt
            .ExcludingMissingMembers()
            .Using<Dictionary<UnitFields, string[]>>(_ => { })
                .WhenTypeIs<Dictionary<UnitFields, string[]>>());
        }

        [Test]
        public void Convert_ShouldReturnExpectedDto()
        {
            _mapper.Convert(_unitSearch).Should().BeEquivalentTo(_unitSearchDto, opt => opt
            .ExcludingMissingMembers()
            .Using<Dictionary<UnitFields, string[]>>(_ => { })
                .WhenTypeIs<Dictionary<UnitFields, string[]>>());
        }
    }
}

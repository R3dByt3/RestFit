using FluentAssertions;
using RestFit.API.Controllers.v1.Mappers;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract;

namespace RestFit.API.Tests.Controllers.v1.Mappers
{
    [TestFixture]
    public class UnitDtoMapperTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;

        private UnitDtoMapper _mapper = null!;
        private Unit _unit = null!;
        private UnitDto _unitDto = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _mapper = new();
            _unit = new Unit
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
            };
            _unitDto = new UnitDto
            {
                DateUtc = Now,
                Id = "abcdfeg",
                UserId = "hijklmno",
                Weight = 5.6,
                Comment = "pqrstuvw",
                Repetitions = 12,
                Sets = 24,
                Type = "12345"
            };
        }

        [Test]
        public void ConvertDto_ShouldReturnExpected()
        {
            _mapper.Convert(_unitDto).Should().BeEquivalentTo(_unit, opt => opt
                .ExcludingMissingMembers()
                .Excluding(ctx => ctx.ProcessedFor));
        }

        [Test]
        public void Convert_ShouldReturnExpectedDto()
        {
            _mapper.Convert(_unit).Should().BeEquivalentTo(_unitDto, opt => opt
                .ExcludingMissingMembers());
        }
    }
}

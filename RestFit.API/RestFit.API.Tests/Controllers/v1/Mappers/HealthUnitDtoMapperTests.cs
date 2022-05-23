using FluentAssertions;
using RestFit.API.Controllers.v1.Mappers;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract;

namespace RestFit.API.Tests.Controllers.v1.Mappers
{
    [TestFixture]
    public class HealthUnitDtoMapperTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;

        private HealthUnitDtoMapper _mapper = null!;
        private HealthUnit _healthUnit = null!;
        private HealthUnitDto _healthUnitDto = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _mapper = new();
            _healthUnit = new HealthUnit
            {
                ArmSize = 1.2,
                DateUtc = Now,
                HipSize = 2.3,
                Id = "abcdfeg",
                ThightSize = 3.4,
                UserId = "hijklmno",
                WaistSize = 4.5,
                Weight = 5.6
            };
            _healthUnitDto = new HealthUnitDto
            {
                ArmSize = 1.2,
                DateUtc = Now,
                HipSize = 2.3,
                Id = "abcdfeg",
                ThightSize = 3.4,
                UserId = "hijklmno",
                WaistSize = 4.5,
                Weight = 5.6
            };
        }

        [Test]
        public void ConvertDto_ShouldReturnExpected()
        {
            _mapper.Convert(_healthUnitDto).Should().BeEquivalentTo(_healthUnit);
        }

        [Test]
        public void Convert_ShouldReturnExpectedDto()
        {
            _mapper.Convert(_healthUnit).Should().BeEquivalentTo(_healthUnitDto);
        }
    }
}

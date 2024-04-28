using FluentAssertions;
using OverlyComplicatedBowling.Application.Games.Mapping;
using OverlyComplicatedBowling.Domain.Games;

namespace OverlyComplicatedBowling.Application.Tests.Games.Mapping
{
    [TestClass]
    public class RollDtoMapperTests
    {
        [TestMethod]
        public void MapDto_MapsRollToRollDto()
        {
            //Arrange
            var roll = new Roll(3, false, true);

            //Act
            var rollDto = RollDtoMapper.MapDto(roll);

            //Assert
            rollDto.KnockedPins.Should().Be(roll.KnockedPins);
            rollDto.IsStrike.Should().Be(roll.IsStrike);
            rollDto.IsSpare.Should().Be(roll.IsSpare);
        }
    }
}

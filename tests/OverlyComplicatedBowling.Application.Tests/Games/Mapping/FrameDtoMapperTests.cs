﻿using FluentAssertions;
using OverlyComplicatedBowling.Application.Games.Mapping;
using OverlyComplicatedBowling.Domain.Games;

namespace OverlyComplicatedBowling.Application.Tests.Games.Mapping
{
	[TestClass]
	public class FrameDtoMapperTests
	{
		[TestMethod]
		public void MapDto_MapsFrameToFrameDto()
		{
			//Arrange
			var game = Game.Start(0);
			game.AddRoll(5);
			var frame = game.Frames.First();

			//Act
			var frameDto = FrameDtoMapper.MapDto(frame);

			//Assert
			frameDto.Rolls.Count.Should().Be(frame.Rolls.Count);
			frameDto.MaxRolls.Should().Be(frame.MaxRolls);
			frameDto.Score.Should().Be(frame.Score);
			frameDto.AccumulatedScore.Should().Be(frame.AccumulatedScore);
			frameDto.Scored.Should().Be(frame.Scored);
			frameDto.Completed.Should().Be(frame.Completed);
			frameDto.RemainingPins.Should().Be(frame.RemainingPins);

		}
	}
}

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OverlyComplicatedBowling.Domain.Games;
using OverlyComplicatedBowling.Infrastructure.Repositories;

namespace OverlyComplicatedBowling.Infrastructure.Tests.Repositories
{
    [TestClass]
    public class PostgreSQLRepositoryTests
    {
        [TestMethod]
        public async Task SaveGameAsync_SavesGame()
        {
            //Arrange
            var dbContextOptions = new DbContextOptionsBuilder<PostgreSQLDbContext>()
                .UseInMemoryDatabase("mockdb")
                .Options;
            var dbContext = new PostgreSQLDbContext(dbContextOptions);
            var repository = new PostgreSQLRepository(dbContext);
            var game = Game.Start();

            //Act
            await repository.SaveGameAsync(game);
            var result = await repository.LoadGameAsync(game.Id);

            //Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task SaveGameAsync_SavesFrames()
        {
            //Arrange
            var dbContextOptions = new DbContextOptionsBuilder<PostgreSQLDbContext>()
                .UseInMemoryDatabase("mockdb")
                .Options;
            var dbContextMock = new PostgreSQLDbContext(dbContextOptions);
            var repository = new PostgreSQLRepository(dbContextMock);
            var game = Game.Start();

            //Act
            await repository.SaveGameAsync(game);
            var result = await repository.LoadGameAsync(game.Id);

            //Assert
            result.Should().NotBeNull();
            result.Frames.Should().NotBeNull();
            result.Frames.First().Key.Should().Be(1);
            result.Frames.First().Value.GetType().Should().Be(typeof(NormalFrame));
            result.Frames.Last().Key.Should().Be(10);
            result.Frames.Last().Value.GetType().Should().Be(typeof(FinalFrame));
        }
    }
}

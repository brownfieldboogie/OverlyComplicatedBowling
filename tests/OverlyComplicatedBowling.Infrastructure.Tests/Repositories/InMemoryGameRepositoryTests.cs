using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OverlyComplicatedBowling.Domain.Games;
using OverlyComplicatedBowling.Infrastructure.Repositories;

namespace OverlyComplicatedBowling.Infrastructure.Tests.Repositories
{
    [TestClass]
    public class InMemoryGameRepositoryTests
    {
        [TestMethod]
        public async Task SaveGameAsync_SavesGame()
        {
            //Arrange
            var repository = new InMemoryGameRepository(Substitute.For<ILogger>());
            var game = Game.Start();

            //Act
            await repository.SaveGameAsync(game);
            var loadedGame = await repository.LoadGameAsync(game.Id);

            //Assert
            loadedGame.Should().NotBeNull();
        }

        [TestMethod]
        public async Task LoadGameAsync_NoGameWithIdExists_ReturnsNull()
        {
            //Arrange
            var repository = new InMemoryGameRepository(Substitute.For<ILogger>());

            //Act
            var loadedGame = await repository.LoadGameAsync(Guid.NewGuid());

            //Assert
            loadedGame.Should().BeNull();
        }
    }
}

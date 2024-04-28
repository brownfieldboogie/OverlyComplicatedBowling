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
        public void SaveGame_SavesGame()
        {
            //Arrange
            var repository = new InMemoryGameRepository(Substitute.For<ILogger>());
            var game = Game.Start();

            //Act
            repository.SaveGame(game);
            var loadedGame = repository.LoadGame(game.Id);

            //Assert
            loadedGame.Should().NotBeNull();
        }

        [TestMethod]
        public void LoadGame_NoGameWithIdExists_ReturnsNull()
        {
            //Arrange
            var repository = new InMemoryGameRepository(Substitute.For<ILogger>());

            //Act
            var loadedGame = repository.LoadGame(Guid.NewGuid());

            //Assert
            loadedGame.Should().BeNull();
        }
    }
}

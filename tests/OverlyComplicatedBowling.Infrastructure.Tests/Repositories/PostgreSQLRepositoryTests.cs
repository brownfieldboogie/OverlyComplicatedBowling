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
            var dbContextMock = new PostgreSQLDbContext(dbContextOptions);
            var repository = new PostgreSQLRepository(dbContextMock);
            var game = Game.Start();

            //Act
            await repository.SaveGameAsync(game);
            var result = await repository.LoadGameAsync(game.Id);

            //Assert
            result.Should().NotBeNull();
        }
    }
}

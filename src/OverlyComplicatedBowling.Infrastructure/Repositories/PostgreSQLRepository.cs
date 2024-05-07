using Microsoft.EntityFrameworkCore;
using OverlyComplicatedBowling.Application.Interfaces;
using OverlyComplicatedBowling.Domain.Games;

namespace OverlyComplicatedBowling.Infrastructure.Repositories
{
    public class PostgreSQLRepository : IGameRepository
    {
        private readonly PostgreSQLDbContext _dbContext;

        public PostgreSQLRepository(PostgreSQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Game?> LoadGameAsync(Guid Id)
        {
            return await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == Id);
        }

        public async Task SaveGameAsync(Game game)
        {
            Game? entry = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == game.Id);

            if (entry is null)
            {
                await _dbContext.Games.AddAsync(game);
            }
            else
            {
                _dbContext.Games.Update(game);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}

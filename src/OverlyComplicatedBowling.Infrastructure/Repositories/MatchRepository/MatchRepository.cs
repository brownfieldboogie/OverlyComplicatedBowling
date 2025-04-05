﻿using Microsoft.EntityFrameworkCore;
using OverlyComplicatedBowling.Application.Interfaces;
using OverlyComplicatedBowling.Domain.Matches;

namespace OverlyComplicatedBowling.Infrastructure.Repositories.MatchRepository
{
	public class MatchRepository : IMatchRepository
	{
		private readonly MatchDbContext _matchDbContext;

		public MatchRepository(MatchDbContext matchDbContext)
		{
			_matchDbContext = matchDbContext;
		}

		public async Task<Match?> LoadMatchAsync(Guid Id)
		{
			var games = await _matchDbContext.Games.ToListAsync();

			return await _matchDbContext.Matches.Include(match => (match as Match).Games).FirstOrDefaultAsync(g => g.Id == Id);
		}

		public async Task SaveMatchAsync(Match match)
		{
			Match? entry = await _matchDbContext.Matches.FirstOrDefaultAsync(g => g.Id == match.Id);

			if (entry is null)
			{
				await _matchDbContext.Matches.AddAsync(match);
				await _matchDbContext.Games.AddRangeAsync([.. match.Games]);
			}
			else
			{
				_matchDbContext.Matches.Update(match);
				_matchDbContext.Games.UpdateRange([.. match.Games]);
			}

			await _matchDbContext.SaveChangesAsync();
		}
	}
}

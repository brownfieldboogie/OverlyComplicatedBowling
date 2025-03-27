using OverlyComplicatedBowling.Application.Games.Mapping;
using OverlyComplicatedBowling.Application.Interfaces;
using OverlyComplicatedBowling.Domain.Matches;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Application.Games.Services
{
	public class MatchService : IMatchService
	{
		private IBowlingRollWebservice _bowlingRollWebservice;
		private readonly IMatchRepository _matchRepository;

		public MatchService(IBowlingRollWebservice bowlingRollWebservice, IMatchRepository matchRepository)
		{
			_bowlingRollWebservice = bowlingRollWebservice;
			_matchRepository = matchRepository;
		}

		public async Task<MatchDto> StartMatchAsync(int numberOfPlayers)
		{
			var match = Match.Start(numberOfPlayers);

			await _matchRepository.SaveMatchAsync(match);

			return MatchDtoMapper.MapDto(match);
		}

		public async Task<MatchDto> AddRollAsync(Guid matchId)
		{
			var match = await _matchRepository.LoadMatchAsync(matchId);

			if (match is null)
			{
				throw new ArgumentNullException(nameof(match));
			}

			var activeGame = match.GetActiveGame();

			if (activeGame.IsGameCompleted())
			{
				return MatchDtoMapper.MapDto(match);
			}

			var remainingPinsOnActiveFrame = activeGame.GetRemainingPinsOnActiveFrame();
			var knockedPins = await _bowlingRollWebservice.GetRollResultAsync(remainingPinsOnActiveFrame);
			match.AddRoll(knockedPins);

			await _matchRepository.SaveMatchAsync(match);

			return MatchDtoMapper.MapDto(match);
		}
	}
}

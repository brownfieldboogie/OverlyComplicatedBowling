using OverlyComplicatedBowling.Application.Games.Mapping;
using OverlyComplicatedBowling.Application.Interfaces;
using OverlyComplicatedBowling.Domain.Games;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Application.Games.Services
{
	public class GameService : IGameService
	{
		private IBowlingRollWebservice _bowlingRollWebservice;
		private readonly IGameRepository _gameRepository;

		public GameService(IBowlingRollWebservice bowlingRollWebservice, IGameRepository gameRepository)
		{
			_bowlingRollWebservice = bowlingRollWebservice;
			_gameRepository = gameRepository;
		}

		public async Task<GameDto> StartGameAsync()
		{
			var game = Game.Start();

			await _gameRepository.SaveGameAsync(game);

			return GameDtoMapper.MapDto(game);
		}

		public async Task<GameDto> AddRollAsync(Guid gameId)
		{
			var game = await _gameRepository.LoadGameAsync(gameId);

			if (game is null)
			{
				throw new ArgumentNullException(nameof(game));
			}

			if (game.IsGameCompleted())
			{
				return GameDtoMapper.MapDto(game);
			}

			var remainingPinsOnActiveFrame = game.GetRemainingPinsOnActiveFrame();
			var knockedPins = await _bowlingRollWebservice.GetRollResultAsync(remainingPinsOnActiveFrame);
			game.AddRoll(knockedPins);

			await _gameRepository.SaveGameAsync(game);

			return GameDtoMapper.MapDto(game);
		}
	}
}

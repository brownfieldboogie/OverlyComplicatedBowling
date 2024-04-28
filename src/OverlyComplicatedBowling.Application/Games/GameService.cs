using OverlyComplicatedBowling.Application.Interfaces;
using OverlyComplicatedBowling.Domain.Games;

namespace OverlyComplicatedBowling.Application.Games
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

            //todo fix mapping
            return new GameDto
            {
                Id = game.Id,
                Frames = game.Frames.ToDictionary(pair => pair.Key,
                    pair => new FrameDto
                    {
                        Rolls = pair.Value.Rolls.ToDictionary(pair => pair.Key,
                            pair => new RollDto
                            {
                                KnockedPins = pair.Value.KnockedPins,
                                IsStrike = pair.Value.IsStrike,
                                IsSpare = pair.Value.IsSpare
                            }),
                        MaxRolls = pair.Value.MaxRolls,
                        Score = pair.Value.Score,
                        Scored = pair.Value.Scored,
                        Completed = pair.Value.Completed,
                        RemainingPins = pair.Value.RemainingPins
                    }),
                GameCompleted = game.IsGameCompleted()
            };
        }

        public async Task<GameDto> AddRollAsync(Guid gameId)
        {
            var game = await _gameRepository.LoadGameAsync(gameId);

            if (game is null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            var remainingPinsOnActiveFrame = game.GetRemainingPinsOnActiveFrame();
            var knockedPins = await _bowlingRollWebservice.GetRollResultAsync(remainingPinsOnActiveFrame);
            game.AddRoll(knockedPins);

            //todo fix mapping
            return new GameDto
            {
                Id = game.Id,
                Frames = game.Frames.ToDictionary(pair => pair.Key,
                    pair => new FrameDto
                    {
                        Rolls = pair.Value.Rolls.ToDictionary(pair => pair.Key,
                            pair => new RollDto
                            {
                                KnockedPins = pair.Value.KnockedPins,
                                IsStrike = pair.Value.IsStrike,
                                IsSpare = pair.Value.IsSpare
                            }),
                        MaxRolls = pair.Value.MaxRolls,
                        Score = pair.Value.Score,
                        Scored = pair.Value.Scored,
                        Completed = pair.Value.Completed,
                        RemainingPins = pair.Value.RemainingPins
                    }),
                GameCompleted = game.IsGameCompleted()
            };
        }
    }
}

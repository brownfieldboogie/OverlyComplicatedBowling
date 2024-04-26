using OverlyComplicatedBowling.Application.Interfaces;
using OverlyComplicatedBowling.Domain.Games;

namespace OverlyComplicatedBowling.Application.Games
{
    public class GameService : IGameService
    {
        private IBowlingRollWebservice _bowlingRollWebservice;

        public GameService(IBowlingRollWebservice bowlingRollWebservice)
        {
            _bowlingRollWebservice = bowlingRollWebservice;
        }

        public GameDto StartGame()
        {
            var game = Game.Start();

            //todo fix mapping
            return new GameDto
            {
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
                    })
            };
        }
    }
}

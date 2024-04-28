using System.Collections.ObjectModel;

namespace OverlyComplicatedBowling.Domain.Games
{
    public class Game
    {
        private Game() { }

        public Guid Id { get; private set; } = Guid.NewGuid();
        internal SortedDictionary<int, Frame> _frames = [];
        public IReadOnlyDictionary<int, Frame> Frames => new ReadOnlyDictionary<int, Frame>(_frames);

        public static Game Start()
        {
            var game = new Game();
            game.CreateFrames();
            return game;
        }

        private void CreateFrames()
        {
            for (int i = 1; i <= 10; i++)
            {
                Frame frame = i == 10 ? FinalFrame.Create() : NormalFrame.Create();
                _frames.Add(i, frame);
            }
        }

        public bool IsGameCompleted()
        {
            return _frames.All(f => f.Value.Completed);
        }

        public int GetRemainingPinsOnActiveFrame()
        {
            return _frames[GetKeyOfAciveFrame()].RemainingPins;
        }

        public void AddRoll(int knockedPins)
        {
            //Add roll to active frame
            //Update score in frames not scored
            _frames[GetKeyOfAciveFrame()].AddRoll(knockedPins);

            for (int i = 1; i <= _frames.Count; i++)
            {
                UpdateScore(i);
            }
        }

        private int GetKeyOfAciveFrame()
        {
            return _frames.First(f => !f.Value.Completed).Key;
        }

        private void UpdateScore(int frameKey)
        {
            var frame = _frames[frameKey];

            if (frame.Scored) return;

            if (frame is NormalFrame)
            {
                var subsequentRolls = _frames.Skip(frameKey).SelectMany(f => f.Value.Rolls.Values).ToArray();
                frame.UpdateScore(subsequentRolls);
            }
            else if (frame is FinalFrame)
            {
                frame.UpdateScore();
            }
        }
    }
}

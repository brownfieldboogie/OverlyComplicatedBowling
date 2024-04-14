using System.Collections.ObjectModel;

namespace OverlyComplicatedBowling.Domain.Games
{
    public class Game
    {
        private Game() { }

        //todo should be the aggregateroot
        //https://code-maze.com/csharp-design-pattern-aggregate/
        //https://github.com/zkavtaskin/Domain-Driven-Design-Example/blob/master/eCommerce/DomainModelLayer/Customers/CreditCard.cs
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
    }
}

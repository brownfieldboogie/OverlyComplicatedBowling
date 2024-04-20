namespace OverlyComplicatedBowling.Domain.Games
{
    public class Frame
    {
        protected Frame() { }

        public virtual SortedDictionary<int, Roll> Rolls { get; protected set; }
        public virtual int MaxRolls { get; protected set; }
        public virtual int Score { get; protected set; }
        public virtual bool Scored { get; protected set; }
        public virtual bool Completed { get; protected set; }

        public virtual void AddRoll(Roll roll) { }
        public virtual void UpdateScore(Roll[]? subsequentRolls = null) { }
    }
}

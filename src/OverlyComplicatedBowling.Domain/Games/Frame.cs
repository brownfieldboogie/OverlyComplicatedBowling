namespace OverlyComplicatedBowling.Domain.Games
{
    public abstract class Frame
    {
        protected Frame() { }

        public virtual SortedDictionary<int, Roll> Rolls { get; protected set; }
        public virtual int MaxRolls { get; protected set; }
        public virtual int Score { get; protected set; }
        public virtual bool Scored { get; protected set; }
        public virtual bool Completed { get; protected set; }
        public virtual int RemainingPins { get; protected set; }

        public abstract void AddRoll(int knockedPins);
        public abstract void UpdateScore(Roll[]? subsequentRolls = null);
    }
}

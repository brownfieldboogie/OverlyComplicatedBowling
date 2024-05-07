namespace OverlyComplicatedBowling.Domain.Games
{
    public abstract class Frame
    {
        protected Frame() { }

        public virtual SortedDictionary<int, Roll> Rolls { get; set; }
        public virtual int MaxRolls { get; set; }
        public virtual int Score { get; set; }
        public virtual bool Scored { get; set; }
        public virtual bool Completed { get; set; }
        public virtual int RemainingPins { get; set; }

        public abstract void AddRoll(int knockedPins);
        public abstract void UpdateScore(Roll[]? subsequentRolls = null);
    }
}

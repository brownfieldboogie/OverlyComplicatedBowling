namespace OverlyComplicatedBowling.Domain.Games
{
    public class Frame
    {
        public virtual SortedDictionary<int, Roll> Rolls { get; protected set; }
        public virtual int MaxRolls { get; protected set; }
        public virtual int Score { get; protected set; }
        public virtual bool Scored { get; protected set; }

        public void AddRoll(Roll roll)
        {
            if (Rolls.Count == MaxRolls) return;

            Rolls.Add(Rolls.Count + 1, roll);
        }

        public void UpdateScore(Roll[]? subsequentRolls = null)
        {
            if (Scored) return;

            if (Rolls.First().Value.IsStrike && subsequentRolls?.Length == 2)
            {
                Score = Rolls.Sum(r => r.Value.KnockedPins) + subsequentRolls.Sum(r => r.KnockedPins);
                Scored = true;
            }
            else if (Rolls.Last().Value.IsSpare && subsequentRolls?.Length == 1)
            {
                Score = Rolls.Sum(r => r.Value.KnockedPins) + subsequentRolls.Sum(r => r.KnockedPins);
                Scored = true;
            }
            else
            {
                Score = Rolls.Sum(r => r.Value.KnockedPins);
                Scored = true;
            }
        }
    }
}

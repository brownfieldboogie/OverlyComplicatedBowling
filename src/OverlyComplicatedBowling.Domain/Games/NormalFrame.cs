namespace OverlyComplicatedBowling.Domain.Games
{
    public class NormalFrame : Frame
    {
        internal static NormalFrame Create()
        {
            return new NormalFrame
            {
                Rolls = [],
                MaxRolls = 2,
                Score = 0,
                Scored = false,
                Completed = false
            };
        }

        public override void AddRoll(Roll roll)
        {
            if (Completed) return;

            Rolls.Add(Rolls.Count + 1, roll);

            if (roll.IsStrike ||
                (roll.IsSpare && Rolls.Count == 2) ||
                Rolls.Count == MaxRolls)
            {
                Completed = true;
            }
        }

        public override void UpdateScore(Roll[]? subsequentRolls = null)
        {
            if (Scored || !Completed) return;

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

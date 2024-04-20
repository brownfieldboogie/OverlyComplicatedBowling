namespace OverlyComplicatedBowling.Domain.Games
{
    public class FinalFrame : Frame
    {
        internal static FinalFrame Create()
        {
            return new FinalFrame
            {
                Rolls = [],
                MaxRolls = 3,
                Score = 0,
                Scored = false,
                Completed = false
            };
        }

        public override void AddRoll(Roll roll)
        {
            if (Completed) return;

            Rolls.Add(Rolls.Count + 1, roll);

            if ((Rolls.Count == 2 && !Rolls.Any(kvp => kvp.Value.IsSpare || kvp.Value.IsStrike)) ||
                Rolls.Count == MaxRolls)
            {
                Completed = true;
            }
        }

        public override void UpdateScore(Roll[]? subsequentRolls = null)
        {
            if (Scored || !Completed) return;

            Score = Rolls.Sum(r => r.Value.KnockedPins);
            Scored = true;
        }
    }
}

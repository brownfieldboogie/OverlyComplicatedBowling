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
                Completed = false,
                RemainingPins = 10
            };
        }

        public override void AddRoll(int knockedPins)
        {
            if (Completed) return;

            RemainingPins -= knockedPins;

            if (Rolls.Count == 0)
            {
                Completed = RemainingPins == 0;
                Rolls.Add(1, new Roll(knockedPins, RemainingPins == 0, false));
            }
            else if (Rolls.Count == 1)
            {
                Completed = true;
                Rolls.Add(2, new Roll(knockedPins, false, RemainingPins == 0));
            }
        }

        public override void UpdateScore(Roll[]? subsequentRolls = null)
        {
            if (Scored || !Completed) return;

            if (Rolls.First().Value.IsStrike && subsequentRolls?.Length >= 2)
            {
                Score = Rolls.Sum(r => r.Value.KnockedPins) + subsequentRolls.Sum(r => r.KnockedPins);
                Scored = true;
            }
            else if (Rolls.Last().Value.IsSpare && subsequentRolls?.Length >= 1)
            {
                Score = Rolls.Sum(r => r.Value.KnockedPins) + subsequentRolls.Sum(r => r.KnockedPins);
                Scored = true;
            }
            else if (!Rolls.Any(r => r.Value.IsStrike || r.Value.IsSpare))
            {
                Score = Rolls.Sum(r => r.Value.KnockedPins);
                Scored = true;
            }
        }
    }
}

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
				AccumulatedScore = 0,
				Scored = false,
				Completed = false,
				RemainingPins = 10
			};
		}

		public override void AddRoll(int knockedPins)
		{
			if (Completed) return;

			if (Rolls.Count == 0)
			{
				var roll = new Roll(knockedPins, knockedPins == 10, false);
				RemainingPins = roll.IsStrike ? 10 : RemainingPins - knockedPins;
				Rolls.Add(1, roll);
			}
			else if (Rolls.Count == 1)
			{
				var isStrike = Rolls[1].IsStrike && knockedPins == 10;
				var isSpare = !Rolls[1].IsStrike && RemainingPins - knockedPins == 0;
				var roll = new Roll(knockedPins, isStrike, isSpare);
				Completed = !Rolls[1].IsStrike && !roll.IsSpare;
				RemainingPins = roll.IsStrike || roll.IsSpare ? 10 : RemainingPins - knockedPins;
				Rolls.Add(2, roll);
			}
			else if (Rolls.Count == 2)
			{
				var isStrike = Rolls[2].IsStrike && knockedPins == 10;
				var isSpare = !Rolls[2].IsStrike && knockedPins == 10;
				var roll = new Roll(knockedPins, isStrike, isSpare);
				Completed = true;
				RemainingPins -= knockedPins;
				Rolls.Add(3, roll);
			}
		}

		public override void UpdateScore(Roll[]? subsequentRolls = null)
		{
			if (Scored || !Completed) return;

			Score = Rolls.Sum(r => r.Value.KnockedPins);
			Scored = true;
		}

		public override void UpdateAccumulatedScore(int accumulatedScore)
		{
			AccumulatedScore = accumulatedScore;
		}
	}
}

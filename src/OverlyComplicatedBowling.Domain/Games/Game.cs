namespace OverlyComplicatedBowling.Domain.Games
{
	public class Game
	{
		public Guid Id { get; private set; } = Guid.NewGuid();
		public List<Frame> Frames { get; set; }
		public int TotalScore;
		public int Index;

		public static Game Start(int index)
		{
			var game = new Game();
			game.Frames = [];
			game.CreateFrames();
			game.TotalScore = 0;
			game.Index = index;
			return game;
		}

		private void CreateFrames()
		{
			for (int i = 0; i <= 9; i++)
			{
				Frame frame = i == 9 ? FinalFrame.Create() : NormalFrame.Create(i);
				Frames.Add(frame);
			}
		}

		public bool IsGameCompleted()
		{
			return Frames.All(f => f.Completed);
		}

		public int GetRemainingPinsOnActiveFrame()
		{
			return Frames[GetIndexOfActiveFrame()].RemainingPins;
		}

		public void AddRoll(int knockedPins)
		{
			Frames[GetIndexOfActiveFrame()].AddRoll(knockedPins);

			for (int i = 0; i <= Frames.Count - 1; i++)
			{
				UpdateScore(i);
			}
		}

		public int GetIndexOfActiveFrame()
		{
			return Frames.FirstOrDefault(f => !f.Completed)?.Index ?? 10;
		}

		private void UpdateScore(int frameIndex)
		{
			var frame = Frames[frameIndex];

			if (frame.Scored) return;

			if (frame is NormalFrame)
			{
				var subsequentRolls = Frames.Skip(frameIndex).SelectMany(f => f.Rolls.Values).ToArray();
				frame.UpdateScore(subsequentRolls);
			}
			else if (frame is FinalFrame)
			{
				frame.UpdateScore();
			}

			var accumulatedScore = Frames.TakeWhile(f => f.Index <= frameIndex).Sum(f => f.Score);
			frame.UpdateAccumulatedScore(accumulatedScore);
			TotalScore = accumulatedScore;
		}
	}
}

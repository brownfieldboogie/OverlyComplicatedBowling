namespace OverlyComplicatedBowling.Domain.Games
{
    public class FinalFrame : Frame
    {
        public static FinalFrame Create()
        {
            return new FinalFrame
            {
                Rolls = [],
                MaxRolls = 3,
                Score = 0,
                Scored = false
            };
        }
    }
}

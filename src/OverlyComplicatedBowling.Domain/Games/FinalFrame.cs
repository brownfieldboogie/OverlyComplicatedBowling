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
                Scored = false
            };
        }
    }
}

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
                Scored = false
            };
        }
    }
}
